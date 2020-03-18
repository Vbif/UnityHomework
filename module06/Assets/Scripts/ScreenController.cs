using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    void TriggerAnimation(string name)
    {
        var anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger(name);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        TriggerAnimation("Show");

        var sound = GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.Play();
        }
    }

    public void Hide()
    {
        TriggerAnimation("Hide");
    }

    public void ShowEnd()
    {
        // dirty trick to make object visible after hide animation change alpha
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void HideEnd()
    {
        gameObject.SetActive(false);
    }
}
