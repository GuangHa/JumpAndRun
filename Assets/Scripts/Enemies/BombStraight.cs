using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyDetection))]
[RequireComponent(typeof(Rigidbody))]
public class BombStraight : MonoBehaviour
{
    public event Action BombExp = delegate { };
    public event Action<int> DoDamage = delegate { };

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float bombSpeed = 3.5f;
    [SerializeField]
    private float timeTillExplode = 4f;
    [SerializeField]
    private int bombDamage = 30;

    private bool launch = false;
    private bool inExplosionRange = false;
    private float elapsedTime = 0f;
    private EnemyDetection ed;
    private Rigidbody rb;
    private GameObject player;
    private Transform bombTransform;


    // Start is called before the first frame update
    void Start()
    {
        ed = GetComponent<EnemyDetection>();
        ed.EnemyDetected += Lunch;
        rb = GetComponent<Rigidbody>();
        bombTransform = transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // starts when lunch = True 
        if (launch)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= timeTillExplode)
            {                
                if (inExplosionRange)
                {
                    DoDamage(bombDamage);                    
                }
                BombExp();
            }           
        }
    }

    private void Lunch()
    {
        launch = true;

        // look at Player
        bombTransform.LookAt(playerTransform);
        // lunch
        rb.velocity = transform.forward * bombSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.root.gameObject == player)
        {
            DoDamage(bombDamage);
            BombExp();          
        }
    }

    // set boolean true if explosion range entered
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.gameObject == player)
        {          
            inExplosionRange = true;
        }
    }

    // set boolean false if explosion range left
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.root.gameObject == player)
        {           
            inExplosionRange = false;
        }
    }
}
