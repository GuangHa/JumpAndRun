using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // We can use this script to give the bullet some initial speed
    private Rigidbody rb;

    [SerializeField]
    private float bulletSpeed = 10;
       
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

}
