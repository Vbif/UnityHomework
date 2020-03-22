using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public bool InTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            GameLogic.Death();
        }
        else if (collision.tag == "Win")
        {
            GameLogic.Win();
        }
        else
        {
            InTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InTrigger = false;
    }
}
