﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void Play()
    {
        GetComponent<Animator>().SetTrigger("Show");
    }
}
