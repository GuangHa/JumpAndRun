using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombNormal : MonoBehaviour
{

    private EnemyDetection ed;

    private Rigidbody rb;

    [SerializeField]
    private float bombSpeed = 0.5f;

    [SerializeField]
    private Transform playerTransform;

    private Transform bombTransform;

    // Start is called before the first frame update
    void Start()
    {
        ed = GetComponent<EnemyDetection>();
        ed.EnemyDetected += Follow;
        rb = GetComponent<Rigidbody>();
        bombTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        bombTransform.LookAt(playerTransform);
    }

    private void Follow()
    {
        rb.velocity = transform.forward * bombSpeed;
        // Do dmg after 5sec or so
    }
}
