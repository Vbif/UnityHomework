using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCommonParams : MonoBehaviour
{
    public Character[] LeftSide;
    public Character[] RightSide;

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
    }

}
