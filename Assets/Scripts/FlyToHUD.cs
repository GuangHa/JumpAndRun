using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToHUD : MonoBehaviour
{
    private void Start()
    {
        GetComponent<PlayerPickUpController>().FlyCoinToHud += FlyCoin;
    }

    private void FlyCoin(GameObject pickUpGameObject)
    {
        //transform.position = Vector3.Lerp(transform.position, hudObject.transform.position, 1.5f * Time.deltaTime);
        // TODO: fly coin to HUD
        Debug.Log(pickUpGameObject);
    }
}
