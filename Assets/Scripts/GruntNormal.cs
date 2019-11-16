using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntNormal : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    private int currentWP = 0;
    [SerializeField]
    private float rotationSpeed = 3f;
    [SerializeField]
    private float speed = 2f;
    private float accuracyWP = 1.0f;
    private GameObject player;
    PlayerHealth playerHealth;
    [SerializeField]
    private int gruntDamage = 20;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length > 0)
        {
            //next waypoint
            if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP){
                currentWP++;
                if(currentWP >= waypoints.Length)
                {
                    currentWP = 0;
                }
            }

            // rotate towards waypoint
            Vector3 direction = waypoints[currentWP].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, 
                                                        Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.root.gameObject == player)
        {
            DoDamage();
        }
    }

    private void DoDamage()
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(gruntDamage);
        }
    }
}
