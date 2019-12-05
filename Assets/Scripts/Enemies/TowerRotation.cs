using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotation : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private EnemyDetection ed;
    private bool aim;

    // Start is called before the first frame update
    void Start()
    {
        ed = GetComponent<EnemyDetection>();
        ed.EnemyDetected += Aim;
        ed.OutOfRange += OutOfRange;
    }

    // Update is called once per frame
    void Update()
    {
        // If in range, look at target
        if (aim)
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPosition);
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
}
