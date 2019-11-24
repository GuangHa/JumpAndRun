using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HauptMenu : MonoBehaviour
{
    // Beim Klicken von PlayButton
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Das Spiel wird gestartet!");
   

    }

    public void SpielVerlassen()
    {
        Debug.Log("Das Spiel wird jetzt beendet!");
        // UnityEngine.Debug.LogError("Das Spiel wird jetzt beendet!"); das ganze wird pausiert
        Application.Quit();
    }
   
}
