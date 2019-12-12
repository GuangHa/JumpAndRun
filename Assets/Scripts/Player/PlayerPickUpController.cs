using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpController : MonoBehaviour
{
    public Text keyCountText;
    public Text coinCountText;
    public GameObject exitObject;
    public event Action<GameObject> ManageCoinCollectionService = delegate { };
    public event Action ManageHealthCollectionService = delegate { };
    public event Action ManageSpeedCollectionService = delegate { };
    public event Action ManageJumpCollectionService = delegate { };
    public event Action ManageKeyCollectionService = delegate { };

    private GameObject pickUpGameObject = null;
    private int powerUpCounter;
    private int keyCount;
    private int coinCount;
    private Animator exitAnimator;

    // Use this for initialization
    void Start()
    {
        exitAnimator = exitObject.GetComponent<Animator>();
        powerUpCounter = 0;
        keyCount = 0;
        coinCount = 0;
        SetStartingKeyCountText();
        SetStartingCoinCountText();
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckPickUps(other, "Key", ManageKeyCollection, ref keyCount);
        CheckPickUps(other, "Coin", ManageCoinCollection, ref coinCount);
        CheckPickUps(other, "HealthUp", ManageHealthCollection, ref powerUpCounter);
        CheckPickUps(other, "SpeedUp", ManageSpeedCollection, ref powerUpCounter);
        CheckPickUps(other, "JumpUp", ManageJumpCollection, ref powerUpCounter);
    }

    private void CheckPickUps(Collider other, String tag, Action callbackMethod, ref int counter)
    {
        if (other.gameObject.CompareTag(tag))
        {
            this.pickUpGameObject = other.gameObject;
            counter++;
            callbackMethod();
        }
    }

    private void DisableObject()
    {
        if (this.pickUpGameObject != null)
        {
            this.pickUpGameObject.SetActive(false);
        }
    }

    private void ManageHealthCollection()
    {
        ManageHealthCollectionService();
        DisableObject();
    }

    private void ManageCoinCollection()
    {
        coinCountText.text = coinCount.ToString() + "x";
        ManageCoinCollectionService(this.pickUpGameObject);
    }

    private void SetStartingCoinCountText()
    {
        coinCountText.text = coinCount.ToString() + "x";
    }

    private void SetStartingKeyCountText()
    {
        keyCountText.text = keyCount.ToString() + "x";
    }

    private void ManageKeyCollection()
    {
        keyCountText.text = keyCount.ToString() + "x";
        if(keyCount >= 3)
        {
            exitAnimator.SetBool("hasAllKeys", true);
            transform.position = new Vector3(12, 0, -2.5f);
            Destroy(exitObject.transform.Find("Interaction").gameObject);
        }
        ManageKeyCollectionService();
        DisableObject();
    }

    private void ManageSpeedCollection()
    {
        ManageSpeedCollectionService();
        DisableObject();
    }

    private void ManageJumpCollection()
    {
        ManageJumpCollectionService();
        DisableObject();
    }
}
