using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    AsyncOperation async;

    public void LoadLevel(int level)
    {
        StartCoroutine(LoadingScreen(level));
    }
    IEnumerator LoadingScreen(int level)
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync(level);
        async.allowSceneActivation = true;
        while (async.isDone == false)
            yield return null;
    }
}
