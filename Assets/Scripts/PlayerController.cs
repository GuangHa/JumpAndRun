using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SoPlayerPosition playerPosition;

    private bool isGrounded = true;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // FOR SINGLE JUMP
    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.tag == ("Ground") && isGrounded == false)
    //    {
    //        isGrounded = true;
    //    }
    //}

    // FOR DOUBLE JUMP
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        playerPosition.position = transform.position;
        playerPosition.player = transform;
    }
}
