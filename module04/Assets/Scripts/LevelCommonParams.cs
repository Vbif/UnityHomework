using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCommonParams : MonoBehaviour
{
    public Character[] LeftSide;
    public Character[] RightSide;
    public LevelManager LevelManager;

    // Start is called before the first frame update
    void Start()
    {
        var logic = GetComponentInChildren<GameLogic>();
        if (logic != null)
        {
            logic.SetCharacters(LeftSide, RightSide);
            logic.Restart();
        }
        else
        {
            Debug.LogError("GameLogic not found");
        }

        var pauseLogic = GetComponentInChildren<PauseLogic>();
        if (pauseLogic != null && LevelManager != null)
        {
            pauseLogic.LevelManager = LevelManager;
        }
    }

}
