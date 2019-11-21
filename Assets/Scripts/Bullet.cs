using System.Collections;
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
            DoDamage();
        }
        Destroy(gameObject);
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
