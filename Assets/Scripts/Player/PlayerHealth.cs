using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private AudioSource playerAudio;
    private bool isDead;
    private bool damaged;

    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        currentHealth = startingHealth;
    }


    private void Update()
    {
        if (damaged)
        {
            // sets the color of the damageImage to flashColour
            damageImage.color = flashColour;
        }
        else
        {
            // color back to clear
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    /// <summary>
    /// Reduces the healthpoints of the player by the given amount.
    /// </summary>
    /// <param name="amount">The amount which the players take as damage.</param>
    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    /// <summary>
    /// Recovers healthpoints of the player by the given amount.
    /// </summary>
    /// <param name="amount">The amount which the player recovers.</param>
    public void RecoverHealth(int amount)
    {
        if(currentHealth < 100)
        {
            if((100 - currentHealth) < 10)
            {
                currentHealth = 100;
            } else
            {
                currentHealth += amount;
            }
            healthSlider.value = currentHealth;
        }
    }

    /// <summary>
    /// Initiates the death of the player.
    /// </summary>
    private void Death()
    {
        isDead = true;

        playerAudio.clip = deathClip;
        playerAudio.Play();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
    }
}
