using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public bool InTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InTrigger = false;
    }
}
