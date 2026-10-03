using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnloadSceneManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] frames;

    [SerializeField] private float interval;
    private EventBus eventBus;

    private Coroutine animationCoroutine = null;

    private WaitForSeconds timer;

    private bool isAnimating = false;

    private void Awake()
    {
        timer = new WaitForSeconds(interval);
    }

    private void Start()
    {
        eventBus = ServiceLocator.Instance.GetService<EventBus>();

        eventBus.Subscribe<OnPlayerUnloadStateChanged>((Action<OnPlayerUnloadStateChanged>)HandleUnloadStateChange);
    }

    private void HandleUnloadStateChange(OnPlayerUnloadStateChanged data)
    {
        if (data.player == player)
        {
            if (data.state)
            {
                UIUtils.SetCanvasState(canvas, true);

                if (animationCoroutine == null)
                {
                    isAnimating = true;
                    animationCoroutine = StartCoroutine(AnimationCoroutine());
                }
            }
            else
            {
                isAnimating = false;
                StopCoroutine(AnimationCoroutine());
                animationCoroutine = null;

                UIUtils.SetCanvasState(canvas, false);
            }
        }
    }

    private IEnumerator AnimationCoroutine()
    {
        int i = 0;
        while (isAnimating)
        {
            image.sprite = frames[i];
            i++;

            if (i >= frames.Length)
            {
                i = 0;
            }

            yield return timer;
        }

        animationCoroutine = null;
    }
}
