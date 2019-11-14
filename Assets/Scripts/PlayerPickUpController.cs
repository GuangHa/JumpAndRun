using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpController : MonoBehaviour
{
    public Text keyCountText;
    public Text coinCountText;
    public Text winText;
    public float speedPowerUp = 2;
    public float jumpHeightPowerUp = 1.0f;
    public int healthRecoveryPowerUp = 10;
    public event Action<GameObject> FlyCoinToHud = delegate { };

    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private IEnumerator coroutine;
    private GameObject pickUpGameObject = null;
    private int powerUpCounter;
    private int keyCount;
    private int coinCount;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
    }

    // Use this for initialization
    void Start()
    {
        powerUpCounter = 0;
        keyCount = 0;
        coinCount = 0;
        SetKeyCountText();
        SetStartingCoinCountText();
        winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        CheckPickUps(other, "Key", SetKeyCountText, ref keyCount);
        CheckPickUps(other, "Coin", SetCoinCountText, ref coinCount);
        CheckPickUps(other, "HealthUp", RecoverHealthPoints, ref powerUpCounter);
        CheckPickUps(other, "SpeedUp", IncreasePlayerSpeed, ref powerUpCounter);
        CheckPickUps(other, "JumpUp", IncreasePlayerJumpHeight, ref powerUpCounter);
    }

    /// <summary>
    /// Checks the pick ups and calls the appropriate method.
    /// </summary>
    /// <param name="other">The collision object</param>
    /// <param name="tag">The object tag</param>
    /// <param name="callbackMethod">The method to call at the end.</param>
    /// <param name="counter">The referenced counter to count the pickups.</param>
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

    /// <summary>
    /// Recovers the healthpoints of the player.
    /// </summary>
    private void RecoverHealthPoints()
    {
        playerHealth.RecoverHealth(healthRecoveryPowerUp);
        DisableObject();
    }

    /// <summary>
    /// Sets the text for counting the coins.
    /// </summary>
    private void SetCoinCountText()
    {
        coinCountText.text = "Coins: " + coinCount.ToString();
        FlyCoinToHud(this.pickUpGameObject);
    }

    private void SetStartingCoinCountText()
    {
        coinCountText.text = "Coins: " + coinCount.ToString();
    }

    /// <summary>
    /// Sets the text for counting the keys.
    /// </summary>
    private void SetKeyCountText()
    {
        keyCountText.text = "Keys found: " + keyCount.ToString();
        if (keyCount >= 3)
        {
            winText.text = "All keys found! You Win!";
        }
        DisableObject();
    }

    /// <summary>
    /// Increaseas the speed of the player.
    /// </summary>
    private void IncreasePlayerSpeed()
    {
        DisableObject();
        playerController.normalSpeed += speedPowerUp;
        playerController.runningSpeed += speedPowerUp;
        coroutine = WaitPowerUpLimitTime(10.0f, ReducePlayerSpeed);
        StartCoroutine(coroutine);
    }

    /// <summary>
    /// Increases the jumping height of the player.
    /// </summary>
    private void IncreasePlayerJumpHeight()
    {
        DisableObject();
        playerController.jumpHeight += jumpHeightPowerUp;
        coroutine = WaitPowerUpLimitTime(20.0f, ReducePlayerJumpHeight);
        StartCoroutine(coroutine);
    }

    /// <summary>
    /// Reduces the jumping height of the player.
    /// </summary>
    private void ReducePlayerJumpHeight()
    {
        playerController.jumpHeight -= jumpHeightPowerUp;
    }

    /// <summary>
    /// Reduces the speed of the player (both walking and running).
    /// </summary>
    private void ReducePlayerSpeed()
    {
        playerController.normalSpeed -= speedPowerUp;
        playerController.runningSpeed -= speedPowerUp;
    }

    /// <summary>
    /// Timelimit for the PowerUps before setting it back to the default value.
    /// </summary>
    /// <param name="waitTime">Duration of the power up.</param>
    /// <param name="setBackToDefaultMethod">Callback method for setting back to default value.</param>
    /// <returns></returns>
    private IEnumerator WaitPowerUpLimitTime(float waitTime, Action setBackToDefaultMethod)
    {
        yield return new WaitForSeconds(waitTime);
        setBackToDefaultMethod();
    }
}
