using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseLogic : MonoBehaviour
{
    public GameObject AllUI;
    public GameObject PauseScreen;

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
}
