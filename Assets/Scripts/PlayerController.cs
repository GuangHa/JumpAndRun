using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SoPlayerPosition playerPosition;
    [SerializeField]
    public float speed = 3.0f;
    [SerializeField]
    public float runningSpeed = 5.0f;
    [SerializeField]
    public float normalSpeed = 3.0f;

    private bool isGrounded = true;
    private bool isRunning = false;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: change player control (mb with wasd?)
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            isRunning = true;
            speed = runningSpeed;
        } else {
            isRunning = false;
            speed = normalSpeed;
        }

        playerPosition.position = transform.position;
        playerPosition.player = transform;
    }
}
