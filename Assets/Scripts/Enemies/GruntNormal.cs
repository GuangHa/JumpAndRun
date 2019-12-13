using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GruntNormal : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float rotationSpeed = 3f;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private int gruntDamage = 20;

    private float accuracyWP = 1.0f;
    private GameObject player;
    private int currentWP = 0;

    public event Action<int> DoDamage = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.root.gameObject == player)
        {
            DoDamage(gruntDamage);
        }
    }    
}
