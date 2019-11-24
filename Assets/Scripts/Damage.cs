using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    PlayerHealth playerHealth;
    private GameObject player;

    private BombStraight bs;
    private BombFollow bf;
    private Bullet bullet;
    private GruntNormal gn;
    private Laser laser;


    // Start is called before the first frame update
    void Start()
    {
        // subscribing to the right dmg source
        bs = GetComponent<BombStraight>();
        if (bs != null)
        {
            bs.DoDamage += DoDamage;
        }

        bf = GetComponent<BombFollow>();
        if (bf != null)
        {
            bf.DoDamage += DoDamage;
        }

        bullet = GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.DoDamage += DoDamage;
        }

        gn = GetComponent<GruntNormal>();
        if (gn != null)
        {
            gn.DoDamage += DoDamage;
        }

        laser = GetComponent<Laser>();
        if (laser != null)
        {
            laser.DoDamage += DoDamage;
        }

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
