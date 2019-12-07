using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    private bool gameIsPaused = false;
    private AudioSource backgroundMusicAudioSource;
    public GameObject backgroundMusicObject;
    public GameObject pauseMenuUI;

    private void Start()
    {
        backgroundMusicAudioSource = backgroundMusicObject.GetComponent<AudioSource>();
    }

    public void LoadMainMenu ()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        backgroundMusicAudioSource.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        backgroundMusicAudioSource.Stop();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

}

