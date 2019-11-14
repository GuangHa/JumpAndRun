using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public event Action EnemyDetected = delegate { };
    [SerializeField]
    private float range;
    [SerializeField]
    private Transform player;

    private void Update()
    {
        if(Vector3.Distance(player.position, transform.position) <= range)
        {
            EnemyDetected();
        }
    }
        
}
