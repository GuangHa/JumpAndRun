using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HauptMenu : MonoBehaviour
{
    // Beim Klicken von PlayButton
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("Das Spiel wird gestartet!");
   

    }

    public void SpielVerlassen()
    {
        Debug.Log("Das Spiel wird jetzt beendet!");
        Application.Quit();
    }
   
}
