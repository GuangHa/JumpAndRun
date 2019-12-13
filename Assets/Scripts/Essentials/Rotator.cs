using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private RotationDirection rotation;

    // Update is called once per frame
    void Update()
    {
        if (rotation == RotationDirection.LeftToRight)
        {
            transform.Rotate(new Vector3(0, 0, -30) * Time.deltaTime);
        }
        else if (rotation == RotationDirection.RightToLeft)
        {
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }
    }
}
