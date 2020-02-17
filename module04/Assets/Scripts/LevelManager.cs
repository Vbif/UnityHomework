using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Slider Slider;
    public float FakeTime;

    private GameObject _main;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _main = gameObject.transform.GetChild(0).gameObject;
    }

    public void LoadLevel(string name)
    {
        StartCoroutine(LoadLevelAsync(name));
    }

    private IEnumerator LoadLevelAsync(string name)
    {
        _main.SetActive(true);

        var loadResult = SceneManager.LoadSceneAsync(name);

        float timer = 0;
        loadResult.allowSceneActivation = false;
        while (loadResult.progress < 0.9f || timer < FakeTime)
        {
            Slider.value = Mathf.Min(loadResult.progress, timer / FakeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        loadResult.allowSceneActivation = true;
        while (!loadResult.isDone)
        {
            Slider.value = loadResult.progress;
            yield return null;
        }

        _main.SetActive(false);
        yield return null;
    }
}
