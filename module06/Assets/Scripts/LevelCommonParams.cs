using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCommonParams : MonoBehaviour
{
    public Character[] LeftSide;
    public Character[] RightSide;
    public LevelManager LevelManager;
    public AudioClip Music;

    // Start is called before the first frame update
    void Start()
    {
        var gameLogic = GetComponentInChildren<GameLogic>();
        if (gameLogic != null)
        {
            gameLogic.SetCharacters(LeftSide, RightSide);
            gameLogic.Restart();
        }
        else
        {
            Debug.LogError("GameLogic not found");
        }

        var screenLogic = GetComponentInChildren<ScreenLogic>();
        if (screenLogic != null && LevelManager != null)
        {
            screenLogic.LevelManager = LevelManager;
        }

        if (Music != null && BackgroundMusic.Instance != null)
        {
            BackgroundMusic.Instance.ChangeMusic(Music);
        }
    }

}
