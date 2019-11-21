using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private EnemyDetection ed;
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private Transform bulletSpawnTransform;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private Transform player;
    private float nextFire;
    private bool aim = false;
    private AudioSource laserBullet;

    // Start is called before the first frame update
    void Start()
    {
        ed = GetComponent<EnemyDetection>();
        ed.EnemyDetected += Aim;
        ed.OutOfRange += OutOfRange;
        laserBullet = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // shoot if aim = true
        if (aim)
        {
            Shoot();
        }
    }

    // If in range set aim = true
    private void Aim()
    {
        aim = true;
    }

    // If out of range set aimt = false
    private void OutOfRange()
    {
        aim = false;
    }


    // instantiating the bullets
    private void Shoot()
    {

        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            bulletSpawnTransform.LookAt(player.position);

            Instantiate(
            bullet,
            bulletSpawnTransform.position,
            bulletSpawnTransform.rotation,
            bulletParent.transform);

            laserBullet.Play();
        }
        
    }
}
