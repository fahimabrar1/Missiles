using Transtions;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public ScaleAndFade playButtonScaler;
    public SlideTransition scoreSlider;
    public SlideTransition gameBannerSlider;
    public SlideTransition cloudSlider;

    private void Start()
    {
        scoreSlider.SlideIn();
        gameBannerSlider.SlideIn();
        cloudSlider.SlideIn();
        playButtonScaler.ScaleAndFadeIn();
    }

    public void OnPlayButtonClicked()
    {
        scoreSlider.SlideOut();
        playButtonScaler.ScaleAndFadeOut();

        gameBannerSlider.SlideOut();
        cloudSlider.SlideOut();

        UIManager.Instance.OnStartGame();
    }
}