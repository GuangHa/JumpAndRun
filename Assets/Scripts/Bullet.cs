﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int attackDamage = 10;
    public float timeBetweenDamage = 0.5f;

    // We can use this script to give the bullet some initial speed
    private Rigidbody rb;
    private GameObject player;
    private bool playerInRange;
    private float timer;
    PlayerHealth playerHealth;

    [SerializeField]
    private float bulletSpeed = 10;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.root.gameObject == player)
        {
            playerInRange = true;
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.transform.root.gameObject == player)
        {
            playerInRange = false;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenDamage && playerInRange)
        {
            DoDamage();
        }

        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            //anim.SetTrigger("PlayerDead");
        }

        // Destroy Bullet after 5 seconds.
        Destroy(gameObject, 5f);
    }

    private void DoDamage()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

}
