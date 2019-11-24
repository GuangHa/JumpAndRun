using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    PlayerHealth playerHealth;
    private GameObject player;

    private BombStraight bs;


    // Start is called before the first frame update
    void Awake()
    {
        bs = GetComponent<BombStraight>();
        //bs.DoDamage += DoDamage;

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoDamage(int dmg)
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(dmg);
            
        }
    }
}
