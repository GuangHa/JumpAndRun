﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HauptMenu : MonoBehaviour
{
    // Beim Klicken von PlayButton
    public void PlayGame()
    {
        SceneManager.LoadScene("PlaygroundScene");
    }

    public void SpielVerlassen()
    {
        Application.Quit();
    }
   
}
