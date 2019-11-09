using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombStraight : MonoBehaviour
{
    private EnemyDetection ed;
    private Rigidbody rb;
    private GameObject player;
    private Transform bombTransform;
    PlayerHealth playerHealth;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float bombSpeed = 3.5f;
    [SerializeField]
    private float timeTillExplode = 4f;
    [SerializeField]
    private int bombDamage = 30;
    private bool lunch = false;
    private bool inExplosionRange = false;
    private float elapsedTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        ed = GetComponent<EnemyDetection>();
        ed.EnemyDetected += Lunch;
        rb = GetComponent<Rigidbody>();
        bombTransform = transform;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        

        // starts  when lunch = True 
        if (lunch)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= timeTillExplode)
            {
                if (inExplosionRange)
                {
                    DoDamage();
                }

                // TODO: explosion animation
                Destroy(gameObject);
            }           
        }
    }

    private void Lunch()
    {
        lunch = true;

        // look at Player
        bombTransform.LookAt(playerTransform);
        // lunch
        rb.velocity = transform.forward * bombSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.root.gameObject == player)
        {
            DoDamage();
            // TODO: explosion animation
            Destroy(gameObject);
        }
    }

    // set boolean true if explosion range entered
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.gameObject == player)
        {
            Debug.Log("Enter Explosion Range");
            inExplosionRange = true;
        }

    }

    // set boolean false if explosion range left
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.root.gameObject == player)
        {
            Debug.Log("Exit Explosion Range");
            inExplosionRange = false;
        }
    }

    private void DoDamage()
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(bombDamage);
        }
    }
}
