using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpService : MonoBehaviour
{
    public int healthRecoveryPowerUp = 10;
    public float speedPowerUp = 2;
    public float jumpHeightPowerUp = 1.0f;

    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private IEnumerator coroutine;
    private GameObject soundManagerObject;
    private SoundManager soundManager;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        GetComponent<PlayerPickUpController>().ManageCoinCollectionService += ManageCoinCollectible;
        GetComponent<PlayerPickUpController>().ManageHealthCollectionService += ManageHealthCollectible;
        GetComponent<PlayerPickUpController>().ManageSpeedCollectionService += ManageSpeedCollectible;
        GetComponent<PlayerPickUpController>().ManageJumpCollectionService += ManageJumpCollectible;
        GetComponent<PlayerPickUpController>().ManageKeyCollectionService += ManageKeyCollectible;
        soundManagerObject = GameObject.FindWithTag("SoundManager");
        soundManager = soundManagerObject.GetComponent<SoundManager>();
    }

    private void ManageCoinCollectible(GameObject pickUpGameObject)
    {
        soundManager.audioSources[0].Play();
        Destroy(pickUpGameObject);
    }

    private void ManageHealthCollectible()
    {
        playerHealth.RecoverHealth(healthRecoveryPowerUp);
        soundManager.audioSources[1].Play();
    }

    private void ManageSpeedCollectible()
    {
        soundManager.audioSources[2].Play();
        playerController.normalSpeed += speedPowerUp;
        playerController.runningSpeed += speedPowerUp;
        coroutine = WaitPowerUpLimitTime(10.0f, ReducePlayerSpeed);
        StartCoroutine(coroutine);
    }

    private void ManageJumpCollectible()
    {
        soundManager.audioSources[3].Play();
        playerController.jumpHeight += jumpHeightPowerUp;
        coroutine = WaitPowerUpLimitTime(20.0f, ReducePlayerJumpHeight);
        StartCoroutine(coroutine);
    }

    private void ManageKeyCollectible()
    {
        soundManager.audioSources[5].Play();
    }

    private void ReducePlayerSpeed()
    {
        playerController.normalSpeed -= speedPowerUp;
        playerController.runningSpeed -= speedPowerUp;
    }

    private void ReducePlayerJumpHeight()
    {
        playerController.jumpHeight -= jumpHeightPowerUp;
    }

    private IEnumerator WaitPowerUpLimitTime(float waitTime, Action setBackToDefaultMethod)
    {
        yield return new WaitForSeconds(waitTime);
        setBackToDefaultMethod();
    }
}
