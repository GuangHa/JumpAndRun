using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public void GotoHauptmenu()
    {
        SceneManager.LoadScene(0);



    }

    public void SpielVerlassen()
    {
        Debug.Log("Das Spiel wird jetzt beendet!");
        // UnityEngine.Debug.LogError("Das Spiel wird jetzt beendet!"); das ganze wird pausiert
        Application.Quit();
    }

}
