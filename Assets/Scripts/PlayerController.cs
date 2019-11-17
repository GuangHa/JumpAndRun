using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float speed = 3.0f;
    [SerializeField]
    public float runningSpeed = 5.0f;
    [SerializeField]
    public float normalSpeed = 3.0f;
    [SerializeField]
    public float jumpHeight = 3.0f;
    [SerializeField]
    public int maxJumps = 2;

    private bool isGrounded = true;
    private Animator anim;
    private int floorMask;
    private int numOfJumps = 0;
    private bool jump = false;
    private float camRayLength = 100f;
    private Rigidbody playerRigidBody;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        
        Move(horizontalInput, verticalInput);
        Running();
        Turning();
        if (jump)
        {
            Jumping();
        }
        Animating(horizontalInput, verticalInput);
    }

    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.transform.root.gameObject.name == "Floor")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.transform.root.gameObject.name == "Floor")
        {
            isGrounded = false;
        }
    }

    /**
     * Player can only move forward or backwards, direction is steered with the mouse
     */
    private void Move(float horizontalValue, float verticalValue)
    {
        transform.Rotate(0, horizontalValue, 0);
        transform.Translate(0, 0, verticalValue);
    }

    private void Running()
    {
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            speed = runningSpeed;
        } else {
            speed = normalSpeed;
        }
    }

    private void Jumping()
    {
        if(isGrounded)
        {
            numOfJumps = 0;
        }
        if ((numOfJumps < maxJumps) || (isGrounded))
        {
            numOfJumps += 1;
            playerRigidBody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            isGrounded = false;
        }
        jump = false;
    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    private void Animating(float horizontalValue, float verticalValue)
    {
        bool walking = horizontalValue != 0f || verticalValue != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
