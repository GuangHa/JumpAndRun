using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Start is called before the first frame update
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
