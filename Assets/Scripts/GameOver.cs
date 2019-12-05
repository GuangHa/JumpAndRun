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

    public void GotoHauptmenu()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void GotoNochmalsSpielen()
    {
        SceneManager.LoadScene("PlaygroundScene");
    }
    
}
