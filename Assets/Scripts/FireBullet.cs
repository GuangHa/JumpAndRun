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

    // Start is called before the first frame update
    void Start()
    {
        ed = GetComponent<EnemyDetection>();
        ed.EnemyDetected += Shoot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        }
        
    }
}
