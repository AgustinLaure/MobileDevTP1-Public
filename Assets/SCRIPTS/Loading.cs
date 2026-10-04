using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private float fillSpeed;
    private EventBus eventBus;

    private float currentFill = 0;

    private bool finishedLoading = false;

    private void Start()
    {
        eventBus = ServiceLocator.Instance.GetService<EventBus>();
    }

    private void Update()
    {
        if (!finishedLoading)
        {
            if (currentFill < 1f)
            {
                currentFill += Time.deltaTime * fillSpeed;
                bar.fillAmount = currentFill;
            }
            else
            {
                currentFill = 1f;
                bar.fillAmount = currentFill;
                finishedLoading = true;
                eventBus.Raise<OnLoadFinished>();
            }
        }
    }
}
