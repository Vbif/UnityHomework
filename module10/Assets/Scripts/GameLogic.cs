using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // for editor
    public GameObject LoseScreen;
    public GameObject WinScreen;

    // events for code

    public event Action OnInit;
    public event Action OnDeath;
    public event Action OnWin;

    // events call

    public static void Init()
    {
        Instance?.OnInit?.Invoke();
    }

    public static void Death()
    {
        Instance?.OnDeath?.Invoke();
    }


    public static void Win()
    {
        Instance?.OnWin?.Invoke();
    }

    public static GameLogic Instance { get; private set; }

    public void Awake()
    {
        Instance = this;

        OnInit += OnInitRoutine;
        OnDeath += OnDeathRoutine;
        OnWin += OnWinRoutine;
    }

    private void OnInitRoutine()
    {
        Time.timeScale = 1.0f;
        LoseScreen?.SetActive(false);
        WinScreen?.SetActive(false);
        Debug.Log("OnInitRoutine");
    }

    private void OnDeathRoutine()
    {
        Time.timeScale = 0.0f;
        LoseScreen?.SetActive(true);
        Debug.Log("OnDeathRoutine");
    }

    private void OnWinRoutine()
    {
        Time.timeScale = 0.0f;
        WinScreen?.SetActive(true);
        Debug.Log("OnWinRoutine");
    }

    public void UnPause()
    {
        Init();
    }
}
