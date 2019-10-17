using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private SoPlayerPosition playerPosition;

    [SerializeField]
    private SoCameraOffset cameraOffset;

    private void Start()
    {
        //transform.position = playerPosition.position + cameraOffset.position;
        //transform.rotation = Quaternion.Euler(cameraOffset.rotation);
    }

    void LateUpdate()
    {
        transform.position = playerPosition.position + cameraOffset.position;
        transform.rotation = Quaternion.Euler(cameraOffset.rotation);
        //transform.LookAt(playerPosition.player);
    }
}
