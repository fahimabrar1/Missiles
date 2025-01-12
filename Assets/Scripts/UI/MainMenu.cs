using TMPro;
using Transtions;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public ScaleAndFade playButtonScaler;
    public SlideTransition scoreSlider;
    public SlideTransition gameBannerSlider;
    public SlideTransition cloudSlider;
    public TMP_Text menuHighScoreText;

    public void Start()
    {
        scoreSlider.SlideIn();
        gameBannerSlider.SlideIn();
        cloudSlider.SlideIn();
        playButtonScaler.gameObject.SetActive(true);
        playButtonScaler.ScaleAndFadeIn();
    }

    public void DelayedStart()
    {
        Start();
    }

    public void OnPlayButtonClicked()
    {
        scoreSlider.SlideOut();
        playButtonScaler.ScaleAndFadeOut();

        gameBannerSlider.SlideOut();
        cloudSlider.SlideOut();

        UIManager.Instance.OnStartGame();
    }

    public void OnShowMainMenu()
    {
        UpdateMenuScore();
        Start();
    }


    public void UpdateMenuScore()
    {
        menuHighScoreText.SetText(PlayerPrefs.GetInt("high_score", 0).ToString());
    }
}