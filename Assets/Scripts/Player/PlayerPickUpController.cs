using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPickUpController : MonoBehaviour
{
    public Text keyCountText;
    public Text coinCountText;
    public event Action<GameObject> ManageCoinCollectionService = delegate { };
    public event Action ManageHealthCollectionService = delegate { };
    public event Action ManageSpeedCollectionService = delegate { };
    public event Action ManageJumpCollectionService = delegate { };
    public event Action ManageKeyCollectionService = delegate { };

    private GameObject pickUpGameObject = null;
    private int powerUpCounter;
    private int keyCount;
    private int coinCount;

    // Use this for initialization
    void Start()
    {
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
        coinCountText.text = "Coins: " + coinCount.ToString();
        ManageCoinCollectionService(this.pickUpGameObject);
    }

    private void SetStartingCoinCountText()
    {
        coinCountText.text = "Coins: " + coinCount.ToString();
    }

    private void SetStartingKeyCountText()
    {
        keyCountText.text= "Key Found: " + keyCount.ToString();
    }

    private void ManageKeyCollection()
    {
        keyCountText.text = "Key Found: " + keyCount.ToString();
        if(keyCount >= 3)
        {
            SceneManager.LoadScene("GameEnd");
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
