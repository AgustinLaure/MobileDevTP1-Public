using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup menuCG;
    [SerializeField] private CanvasGroup creditsCG;
    [SerializeField] private CanvasGroup difficultyCG;

    [SerializeField] private Button playButton;
    [SerializeField] private Button playMultiplayerButton;
    [SerializeField] private Button creditsButton;

    [SerializeField] private Button backButton;

    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;

    private EventBus eventBus;

    private void OnEnable()
    {
        playButton.onClick.AddListener(HandlePlayButton);
        playMultiplayerButton.onClick.AddListener(HandlePlayMultiplayerButton);
        creditsButton.onClick.AddListener(HandleCreditsButton);

        backButton.onClick.AddListener(HandleCreditsBackButton);

        easyButton.onClick.AddListener(HandleEasyButton);
        mediumButton.onClick.AddListener(HandleMediumButton);
        hardButton.onClick.AddListener(HandleHardButton);
    }

    private void Start()
    {
        eventBus = ServiceLocator.Instance.GetService<EventBus>();
    }

    private void HandlePlayButton()
    {
        UIUtils.SetCanvasState(menuCG, false);
        UIUtils.SetCanvasState(difficultyCG, true);
        DatosPartida.isSinglePlayer = true;
    }

    private void HandlePlayMultiplayerButton()
    {
        UIUtils.SetCanvasState(menuCG, false);
        UIUtils.SetCanvasState(difficultyCG, true);
        DatosPartida.isSinglePlayer = false;
    }
    private void HandleCreditsButton()
    {
        UIUtils.SetCanvasState(menuCG, false);
        UIUtils.SetCanvasState(creditsCG, true);
    }

    private void HandleCreditsBackButton()
    {
        UIUtils.SetCanvasState(creditsCG, false);
        UIUtils.SetCanvasState(menuCG, true);
    }

    private void HandleEasyButton()
    {
        eventBus.Raise<OnDifficultySelected>(Difficulty.Easy);
        DatosPartida.difficulty = Difficulty.Easy;
        Play();
    }

    private void HandleMediumButton()
    {
        eventBus.Raise<OnDifficultySelected>(Difficulty.Medium);
        DatosPartida.difficulty = Difficulty.Medium;
        Play();
    }

    private void HandleHardButton()
    {
        eventBus.Raise<OnDifficultySelected>(Difficulty.Hard);
        DatosPartida.difficulty = Difficulty.Hard;
        Play();
    }

    private void Play()
    {
        Application.LoadLevel(1);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(HandlePlayButton);
        playMultiplayerButton.onClick.RemoveListener(HandlePlayMultiplayerButton);
        creditsButton.onClick.RemoveListener(HandleCreditsButton);

        backButton.onClick.RemoveListener(HandleCreditsBackButton);

        easyButton.onClick.RemoveListener(HandleEasyButton);
        mediumButton.onClick.RemoveListener(HandleMediumButton);
        hardButton.onClick.RemoveListener(HandleHardButton);
    }
}
