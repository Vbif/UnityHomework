using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void Show()
    {
        GetComponent<Animator>().Play("show");
    }

    public void Hide()
    {
        GetComponent<Animator>().Play("hide");
    }
}
