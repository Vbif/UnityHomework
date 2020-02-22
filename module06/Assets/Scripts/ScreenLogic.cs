using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLogic : MonoBehaviour
{
    public GameObject AllUI;
    public GameObject PauseScreen;
    public GameObject LoseScreen;
    public GameObject WinScreen;
    public LevelManager LevelManager;

    public void Pause()
    {
        if (AllUI != null)
        {
            AllUI.SetActive(false);
        }

        if (PauseScreen != null)
        {
            PauseScreen.SetActive(true);
        }

        Time.timeScale = 0;
    }

    public void UnPause()
    {
        if (AllUI != null)
        {
            AllUI.SetActive(true);
        }

        if (PauseScreen != null)
        {
            PauseScreen.SetActive(false);
        }

        Time.timeScale = 1;
    }

    public void ToMainMenu()
    {
        if (LevelManager != null)
        {
            LevelManager.LoadLevel("MainMenu");
        }
        else
        {
            Debug.Log("This option not configured");
        }
    }

    public void Win()
    {
        if (AllUI != null)
        {
            AllUI.SetActive(false);
        }

        if (WinScreen != null)
        {
            WinScreen.SetActive(true);
        }
    }

    public void Lose()
    {
        if (AllUI != null)
        {
            AllUI.SetActive(false);
        }

        if (LoseScreen != null)
        {
            LoseScreen.SetActive(true);
        }
    }
}
