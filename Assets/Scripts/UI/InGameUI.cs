using System.Threading.Tasks;
using DG.Tweening;
using Prefabs.Timer;
using TMPro;
using Transtions;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public ScaleAndFade resumeButtonScaler;
    public ScaleAndFade pauseButtonScaler;
    public SlideTransition headerSlider;

    [Header("Game Over")] public GameObject gameOverPanel;

    public ScaleAndFade gameOverScaler;
    public ScaleAndFade playAgainScaler;
    public ScaleAndFade row1Scaler;
    public ScaleAndFade row2Scaler;
    public ScaleAndFade row3Scaler;

    public TMP_Text largeScoreText, starText, clockText, topScoreText, scoreText;
    public StopwatchTimer Stopwatch;
    public int score;


    public int Score
    {
        get => score;
        set
        {
            score = value;
            starText.SetText(score.ToString());
            topScoreText.SetText(score.ToString());
        }
    }

    public void OnEnableUI()
    {
        Score = 0;
        Stopwatch.StartTimer();
        pauseButtonScaler.ScaleAndFadeIn();

        resumeButtonScaler.transform.localScale = Vector3.zero;
        resumeButtonScaler.gameObject.SetActive(false);

        headerSlider.gameObject.SetActive(true);
        headerSlider.SlideIn();
    }

    public void OnDisableUI()
    {
        headerSlider.SlideOut(() =>
        {
            headerSlider.transform.DOKill();
            headerSlider.gameObject.SetActive(false);
        });

        resumeButtonScaler.ScaleAndFadeOut(() => { resumeButtonScaler.transform.DOKill(); });
    }

    public void OnPauseGame()
    {
        Time.timeScale = 0;
        pauseButtonScaler.ScaleAndFadeOut();

        resumeButtonScaler.gameObject.SetActive(true);
        resumeButtonScaler.ScaleAndFadeIn();
        UIManager.Instance.bottomMenu.OnShowButtons();
    }

    public void OnResumeGame()
    {
        Time.timeScale = 1;
        pauseButtonScaler.gameObject.SetActive(true);
        pauseButtonScaler.ScaleAndFadeIn();

        resumeButtonScaler.ScaleAndFadeOut();
        UIManager.Instance.bottomMenu.OnHideButtons();
    }

    public async void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);
        pauseButtonScaler.ScaleAndFadeOut();
        playAgainScaler.gameObject.SetActive(false);
        CalculateGameScore();
        gameOverScaler.ScaleAndFadeIn();
        await Task.Delay(2000);
        row1Scaler.ScaleAndFadeIn();
        await Task.Delay(500);
        row2Scaler.ScaleAndFadeIn();
        await Task.Delay(500);
        row3Scaler.ScaleAndFadeIn();
        await Task.Delay(1000);
        playAgainScaler.gameObject.SetActive(true);
        playAgainScaler.ScaleAndFadeIn();
        UIManager.Instance.bottomMenu.OnShowButtons();
    }

    public async void HideGameOverUI()
    {
        ResetAllScores();
        gameOverScaler.ScaleAndFadeOut();
        row1Scaler.ScaleAndFadeOut();

        row2Scaler.ScaleAndFadeOut();

        row3Scaler.ScaleAndFadeOut();
        playAgainScaler.ScaleAndFadeOut();
        await Task.Delay(1000);
        gameOverPanel.SetActive(false);
    }

    private void ResetAllScores()
    {
        Score = 0;
        Stopwatch.ResetTimer();
        scoreText.SetText("0");
        largeScoreText.SetText("0");
    }

    private void CalculateGameScore()
    {
        Stopwatch.StopTimer();
        var totalSecond = Stopwatch.GetSeconds();
        clockText.text = totalSecond.ToString();
        var _score = totalSecond + score * 10;
        largeScoreText.text = _score.ToString();
        scoreText.text = _score.ToString();
        var hs = PlayerPrefs.GetInt("high_score", 0);
        if (hs < _score)
            PlayerPrefs.SetInt("high_score", _score);
    }

    public void UpdateScore(int pointValue)
    {
        Score += pointValue;
    }
}