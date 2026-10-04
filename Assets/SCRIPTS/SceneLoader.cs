using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private float fadeTime = 0.7f;

    [SerializeField] private Image fade;
    private EventBus eventBus;

    private Coroutine loadSceneCoroutine = null;

    private const string loadScreenName = "LoadingScreen";

    private bool loadingAnimEnded = false;
    private float loadSceneDelay = 1.3f;

    private void Awake()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        eventBus = ServiceLocator.Instance.GetService<EventBus>();
        eventBus.Subscribe<OnLoadFinished>((Action)HandleLoadAnimEnded);
    }

    public void LoadScene(string sceneName, bool shouldDelay)
    {
        if (loadSceneCoroutine == null)
        {
            loadSceneCoroutine = StartCoroutine(LoadSceneCoroutine(sceneName, shouldDelay));
        }
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, bool shouldDelay)
    {
        yield return FadeInCoroutine();
        loadingAnimEnded = false;

        AsyncOperation loadScreenOp = SceneManager.LoadSceneAsync(loadScreenName);
        while (!loadScreenOp.isDone)
        {
            yield return null;
        }

        yield return FadeOutCoroutine();

        AsyncOperation targetSceneOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!targetSceneOp.isDone)
        {
            yield return null;
        }

        yield return new WaitUntil(() => loadingAnimEnded);

        if (shouldDelay)
        {
            yield return new WaitForSeconds(loadSceneDelay);
        }

        yield return FadeInCoroutine();

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(loadScreenName);
        while (!unloadOp.isDone)
        {
            yield return null;
        }

        yield return FadeOutCoroutine();

        loadSceneCoroutine = null;
    }

    private void HandleLoadAnimEnded()
    {
        loadingAnimEnded = true;
    }

    private IEnumerator FadeInCoroutine()
    {
        float i = fade.color.a;
        while (i < 1f)
        {
            i += Time.deltaTime / fadeTime;

            Color color = fade.color;
            color.a = i;
            fade.color = color;

            yield return null;
        }

        Color finalColor = fade.color;
        finalColor.a = 1f;
        fade.color = finalColor;
    }

    private IEnumerator FadeOutCoroutine()
    {
        float i = fade.color.a;
        while (i > 0f)
        {
            i -= Time.deltaTime / fadeTime;

            Color color = fade.color;
            color.a = i;
            fade.color = color;

            yield return null;
        }

        Color finalColor = fade.color;
        finalColor.a = 0f;
        fade.color = finalColor;
    }
}