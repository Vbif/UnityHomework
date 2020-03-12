using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic _instance;

    public static BackgroundMusic Instance => _instance;

    public AudioClip Music;
    public AudioMixerGroup Group;

    private AudioSource _current;
    private AudioSource _prev;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            MagicInit();
            ChangeMusic(Music);
        }
        else
        {
            _instance.ChangeMusic(Music);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    void MagicInit()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        var camera = Camera.main;
        if (camera != null) {
            transform.position = camera.transform.position;
        }

        _current = CreateAudioSource();
        _prev = CreateAudioSource();
    }

    AudioSource CreateAudioSource()
    {
        var ans = gameObject.AddComponent<AudioSource>();
        ans.loop = true;
        ans.playOnAwake = false;
        ans.outputAudioMixerGroup = Group;
        ans.volume = 0;
        return ans;
    }

    public void ChangeMusic(AudioClip audioClip)
    {
        if (_current == null || _prev == null)
        {
            Debug.Log("BackgroundMusic not initilized");
            return;
        }

        if (audioClip == null)
        {
            return;
        }

        StopAllCoroutines();
        _prev.Stop();
        _prev.volume = 0;

        StartCoroutine(AudioController.FadeOut(_current, 1));

        var temp = _current;
        _current = _prev;
        _prev = temp;

        _current.clip = audioClip;
        StartCoroutine(AudioController.FadeIn(_current, 1));
    }
}
