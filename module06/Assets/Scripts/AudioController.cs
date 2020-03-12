using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from https://medium.com/@wyattferguson/how-to-fade-out-in-audio-in-unity-8fce422ab1a8
public static class AudioController
{
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.unscaledDeltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.unscaledDeltaTime / FadeTime;
            yield return null;
        }
    }
}