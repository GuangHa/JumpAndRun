using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    private GameObject player;
    private bool dmgReset;
    private float timer;
    [SerializeField]
    private int laserDmg = 20;   
    [SerializeField]
    private float laserDmgCD = 2f;

    public event Action<int> DoDamage = delegate { };

    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");       
    }

    // Update is called once per frame
    void Update()
    {
        //reset laserDmg after certain amount of time (2s)
        if (dmgReset == true)
        {
            timer += Time.deltaTime;
            if(timer >= laserDmgCD)
            {
                dmgReset = false;
                timer = 0f;
            }
        }

        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);

                if (hit.collider.gameObject.GetComponent<PlayerController>() && dmgReset == false)
                {
                    DoDamage(laserDmg);
                    dmgReset = true;
                }
            }
        }
        else
        {
            lr.SetPosition(1, transform.forward * 5000);
        }
    }    
}