using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    private void Start()
    {
        PlayerPrefs.SetString("LastSceneName", SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("PlaygroundScene");
    }
    
}
