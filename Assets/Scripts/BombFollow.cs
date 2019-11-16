using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFollow : MonoBehaviour
{

    private EnemyDetection ed;
    private Rigidbody rb;
    private GameObject player;
    private Transform bombTransform;
    PlayerHealth playerHealth;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float bombSpeed = 1.5f;
    [SerializeField]
    private float stopRange = 1.5f;
    [SerializeField]
    private float timeTillExplode = 4f;
    [SerializeField]
    private int bombDamage = 30;
    private bool follow = false;    
    private bool inExplosionRange = false;
    private float elapsedTime = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        ed = GetComponent<EnemyDetection>();
        ed.EnemyDetected += Follow;
        rb = GetComponent<Rigidbody>();
        bombTransform = transform;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // looks always at Player
        bombTransform.LookAt(playerTransform);

        // starts following when follow = True and stops when close to player (stopRange)
        if (follow)
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

            if (Vector3.Distance(playerTransform.position, bombTransform.position) >= stopRange)
            {
                rb.velocity = transform.forward * bombSpeed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }       
    }

    private void Follow()
    {
        follow = true;     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.gameObject == player)
        {
            //Debug.Log("Enter Explosion Range");
            inExplosionRange = true;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.root.gameObject == player)
        {
            //Debug.Log("Exit Explosion Range");
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
