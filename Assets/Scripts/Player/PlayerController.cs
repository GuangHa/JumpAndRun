using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float runningSpeed = 5.0f;
    public float normalSpeed = 3.0f;
    public float jumpHeight = 3.0f;

    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private float slowingRate = 1.0f;
    [SerializeField]
    private int maxJumps = 2;

    private bool isGrounded = true;
    private bool isSlow = false;
    private Animator anim;
    private int floorMask;
    private int numOfJumps = 0;
    private bool jump = false;
    private float camRayLength = 100f;
    private Rigidbody playerRigidBody;
    private GameObject soundManagerObject;
    private SoundManager soundManager;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        soundManagerObject = GameObject.FindWithTag("SoundManager");
        soundManager = soundManagerObject.GetComponent<SoundManager>();
        
        // play sound here_we_go_again
        if(PlayerPrefs.GetString("LastSceneName") == "GameOver")
        {
            soundManager.audioSources[8].Play();
        }
        // reset the string
        PlayerPrefs.SetString("LastSceneName", SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.root.gameObject.name == "Floor")
        {
            if(collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                if(Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    isGrounded = true;
                }
                if(collision.gameObject.CompareTag("Ice"))
                {
                    isSlow = true;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform.root.gameObject.name == "Floor")
        {
            isGrounded = false;
            if (collision.gameObject.CompareTag("Ice"))
            {
                isSlow = false;
            }
        }
    }

    private void Move(float horizontalValue, float verticalValue)
    {
        transform.Translate(horizontalValue, 0, 0);
        transform.Translate(0, 0, verticalValue);
    }

    private void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            if (isSlow)
            {
                speed = runningSpeed - slowingRate;
            }
            else
            {
                speed = runningSpeed;
            }
        } else {
            if (isSlow)
            {
                speed = normalSpeed - slowingRate;
            }
            else
            {
                speed = normalSpeed;
            }
        }
    }

    private void Jumping()
    {
        if (isGrounded)
        {
            numOfJumps = 0;
        }
        if ((numOfJumps < maxJumps) || (isGrounded))
        {
            soundManager.audioSources[4].Play();
            numOfJumps += 1;
            playerRigidBody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            isGrounded = false;
        }
        // allows jumping again, if the player is somehow not completly on the ground
        if(numOfJumps >= maxJumps && !isGrounded && transform.position.y < 0.1)
        {
            isGrounded = true;
        }
        jump = false;
    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
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