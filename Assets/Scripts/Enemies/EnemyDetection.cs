using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public event Action EnemyDetected = delegate { };
    public event Action OutOfRange = delegate { };

    [SerializeField]
    private float range;
    [SerializeField]
    private Transform player;
    private bool detected=false;

    private void Update()
    {
        if(Vector3.Distance(player.position, transform.position) <= range && detected == false)
        {
            detected = true;
            EnemyDetected();
        }

        if (Vector3.Distance(player.position, transform.position) >= range && detected == true)
        {
            detected = false;
            OutOfRange();
        }
    } 
        
}
