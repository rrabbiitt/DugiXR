using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    public LoadData loadScenedata;

    public float leakTime;

    void Start()
    {
        loadScenedata = FindObjectOfType<LoadData>();

        if (loadScenedata.IsClear)
        {
            StartCoroutine(TimeLeak(leakTime));
        }
        else if (loadScenedata.NextScene == "Town")
        {
            StartCoroutine(TimeLeak(0));
        }
        else if (loadScenedata.NextScene == "Ending")
        {
            StartCoroutine(TimeLeak(75.0f));
        }
        else
        {
            StartCoroutine(TimeLeak(leakTime));
        }
    }

    IEnumerator Loading()
    {
        if (loadScenedata.NextScene == "Ending")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        else
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(loadScenedata.NextScene);

            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }
    IEnumerator TimeLeak(float i)
    {
        yield return new WaitForSeconds(i);
        yield return new WaitForEndOfFrame();
        StartCoroutine(Loading());
    }

    public void TitleStart()
    {
        SceneManager.LoadScene("MainStart");
    }

    public void TitleEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}