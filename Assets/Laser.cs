using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;

    private GameObject player;
    PlayerHealth playerHealth;

    [SerializeField]
    private int laserDmg = 20;

    private bool dmgReset;
 
    private float timer;
    [SerializeField]
    private float laserDmgCD = 2f;

    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
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
                    DoDamage();
                    dmgReset = true;
                    Debug.Log("dmg");
                }
            }
        }
        else lr.SetPosition(1, transform.forward * 5000);
    }

    private void DoDamage()
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(laserDmg);
        }
    }
}