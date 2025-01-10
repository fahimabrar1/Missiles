using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public RectTransform gameBanner; // Reference to the game banner RectTransform
    public RectTransform cloud; // Reference to the cloud RectTransform
    public Button playButton; // Reference to the play button
    public CanvasGroup playButtonCanvasGroup; // Reference to play button CanvasGroup for fading


    private Vector3 bannerOffScreenDown; // Position for slide-down off the screen
    private Vector3 bannerOffScreenLeft; // Position for slide-out to the left

    private Vector3 bannerStartPosition; // Initial position of the banner

    private void Start()
    {
        // Store the banner's initial position
        bannerStartPosition = gameBanner.anchoredPosition;

        // Calculate the off-screen positions
        bannerOffScreenLeft = bannerStartPosition + new Vector3(-Screen.width, 0, 0); // Off-screen to the left
        bannerOffScreenDown = bannerStartPosition + new Vector3(0, -Screen.height, 0); // Off-screen downward

        // Move the banner off-screen to the left initially
        gameBanner.anchoredPosition = bannerOffScreenLeft;

        // Add listener for the play button
        playButton.onClick.AddListener(OnPlayButtonClicked);

        // Trigger the animations
        SlideInBanner(gameBanner);
        ShowPlayButton();
    }

    public void SlideInBanner(RectTransform rectTransform)
    {
        // Slide the banner into the screen
        rectTransform.DOAnchorPos(bannerStartPosition, 1f).SetEase(Ease.OutBack);
    }

    public void SlideOutLeftBanner(RectTransform rectTransform)
    {
        // Slide the banner out of the screen to the left
        rectTransform.DOAnchorPos(bannerOffScreenLeft, 1f).SetEase(Ease.InBack);
    }

    public void SlideOutDownBanner(RectTransform rectTransform)
    {
        // Slide the banner downward off the screen
        cloud.DOAnchorPos(bannerOffScreenDown, 1f).SetEase(Ease.InBack);
    }

    private void ShowPlayButton()
    {
        playButton.interactable = true;
        // Scale up and fade in the play button
        playButtonCanvasGroup.alpha = 0;
        playButton.transform.localScale = Vector3.zero;

        playButtonCanvasGroup.DOFade(1, 1f).SetEase(Ease.InOutSine);
        playButton.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
    }

    private void HidePlayButton()
    {
        playButton.interactable = false;
        // Scale down and fade out the play button
        playButton.transform.DOScale(Vector3.one * 5, 0.5f).SetEase(Ease.InBack);
        playButtonCanvasGroup.DOFade(0, 0.5f).SetEase(Ease.InSine).OnComplete(() =>
        {
            playButton.gameObject.SetActive(false);
        });
    }

    private void OnPlayButtonClicked()
    {
        // Deactivate the button with animations, then slide out the banner
        HidePlayButton();
        SlideOutLeftBanner(gameBanner);
        SlideOutDownBanner(cloud); // You can change to SlideOutBanner() if needed

        UIManager.Instance.OnStartGame();
    }
}