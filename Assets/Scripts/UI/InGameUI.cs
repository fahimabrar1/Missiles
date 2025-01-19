using System.Collections;
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
    public GameObject joyStick;

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
            starText.SetText((score * 10).ToString());
            topScoreText.SetText(score.ToString());
        }
    }

    public void OnEnableUI()
    {
        Score = 0;
        joyStick.SetActive(true);

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
        joyStick.SetActive(false);
        resumeButtonScaler.gameObject.SetActive(true);
        resumeButtonScaler.ScaleAndFadeIn();
        UIManager.Instance.bottomMenu.OnShowButtons();
    }

    public void OnResumeGame()
    {
        Time.timeScale = 1;
        joyStick.SetActive(true);

        pauseButtonScaler.gameObject.SetActive(true);
        pauseButtonScaler.ScaleAndFadeIn();

        resumeButtonScaler.ScaleAndFadeOut();
        UIManager.Instance.bottomMenu.OnHideButtons();
    }

    public void ShowGameOverUI()
    {
        CalculateGameScore();

        StartCoroutine(showGameOverUI());

        IEnumerator showGameOverUI()
        {
            joyStick.SetActive(false);
            gameOverPanel.SetActive(true);
            pauseButtonScaler.ScaleAndFadeOut();
            playAgainScaler.gameObject.SetActive(false);
            gameOverScaler.ScaleAndFadeIn();
            yield return new WaitForSeconds(2f);
            row1Scaler.ScaleAndFadeIn();
            yield return new WaitForSeconds(0.5f);
            row2Scaler.ScaleAndFadeIn();
            yield return new WaitForSeconds(0.5f);
            row3Scaler.ScaleAndFadeIn();
            yield return new WaitForSeconds(1);
            playAgainScaler.gameObject.SetActive(true);
            playAgainScaler.ScaleAndFadeIn();
            UIManager.Instance.bottomMenu.OnShowButtons();
        }
    }

    public void HideGameOverUI()
    {
        StartCoroutine(hideGameOverUI());

        IEnumerator hideGameOverUI()
        {
            joyStick.SetActive(false);
            ResetAllScores();
            gameOverScaler.ScaleAndFadeOut();
            row1Scaler.ScaleAndFadeOut();

            row2Scaler.ScaleAndFadeOut();

            row3Scaler.ScaleAndFadeOut();
            playAgainScaler.ScaleAndFadeOut();
            yield return new WaitForSeconds(1);
            gameOverPanel.SetActive(false);
        }
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
        var _score = totalSecond + Score * 10;
        largeScoreText.text = _score.ToString();
        scoreText.text = _score.ToString();
        var hs = PlayerPrefs.GetInt("high_score", 0);
        if (hs < _score)
        {
            PlayerPrefs.SetInt("high_score", _score);
            PlayerPrefs.Save();
        }
    }

    public void UpdateScore(int pointValue)
    {
        Score += pointValue;
    }
}