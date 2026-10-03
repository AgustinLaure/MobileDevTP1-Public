using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup[] truckImages;
    [SerializeField] private CanvasGroup fullTruckHighlight;
    [SerializeField] private CanvasGroup waitingText;
    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private float flashInterval;

    private int currentTruckImage = 0;

    private EventBus eventBus;

    private Coroutine flashHighlight = null;
    private WaitForSeconds highlightWait;

    private void Awake()
    {
        UIUtils.SetCanvasState(truckImages[0], true);
        currentTruckImage = 0;

        highlightWait = new WaitForSeconds(flashInterval);
    }

    private void Start()
    {
        eventBus = ServiceLocator.Instance.GetService<EventBus>();
        eventBus.Subscribe<OnBagCollected>((Action<OnBagCollected>)HandleBagCollected);
        eventBus.Subscribe<OnPlayerWaitingTextShouldUpdate>((Action<OnPlayerWaitingTextShouldUpdate>)HandlePlayerWaitingTextUpdate);
        eventBus.Subscribe<OnPlayerMoneyUpdated>((Action<OnPlayerMoneyUpdated>)HandlePlayerMoneyUpdated);
        transform.SetParent(null);
    }

    private void Update()
    {
        if (currentTruckImage == truckImages.Length - 1)
        {
            if (flashHighlight == null)
            {
                flashHighlight = StartCoroutine(FlashHighlight());
            }
        }
        else
        {
            if (flashHighlight != null)
            {
                StopCoroutine(flashHighlight);
                flashHighlight = null;

                UIUtils.SetCanvasState(truckImages[truckImages.Length - 1], false);
                UIUtils.SetCanvasState(fullTruckHighlight, false);
            }
        }
    }

    private void HandleBagCollected(OnBagCollected data)
    {
        if (data.player == player)
        {
            int bagsAmount = data.bagsAmount;

            if (bagsAmount >= 0 && bagsAmount < truckImages.Length)
            {
                UIUtils.SetCanvasState(truckImages[currentTruckImage], false);
                currentTruckImage = bagsAmount;
                UIUtils.SetCanvasState(truckImages[currentTruckImage], true);
            }
        }
    }

    private void HandlePlayerWaitingTextUpdate(OnPlayerWaitingTextShouldUpdate data)
    {
        if (data.player == player)
        {
            UIUtils.SetCanvasState(waitingText, data.state);
        }
    }

    private void HandlePlayerMoneyUpdated(OnPlayerMoneyUpdated data)
    {
        if (data.player == player)
        {
            moneyText.text = "$" + data.money.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("es-AR"));
        }
    }

    private IEnumerator FlashHighlight()
    {
        UIUtils.SetCanvasState(truckImages[truckImages.Length - 1], false);
        UIUtils.SetCanvasState(fullTruckHighlight, true);
        yield return highlightWait;

        UIUtils.SetCanvasState(fullTruckHighlight, false);
        UIUtils.SetCanvasState(truckImages[truckImages.Length - 1], true);

        yield return highlightWait;

        flashHighlight = null;
    }

    public void SetCanvasCamera(Camera camera)
    {
        canvas.worldCamera = camera;
    }
}
