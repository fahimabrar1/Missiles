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

    [Header("Game Over")] public ScaleAndFade gameOverScaler;

    public ScaleAndFade row1Scaler;
    public ScaleAndFade row2Scaler;
    public ScaleAndFade row3Scaler;

    public TMP_Text starText, clockText, scoreText;
    public StopwatchTimer Stopwatch;

    public void OnEnableUI()
    {
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
        CaculateGameScore();
        gameOverScaler.ScaleAndFadeIn();
        await Task.Delay(2000);
        row1Scaler.ScaleAndFadeIn();
        await Task.Delay(500);
        row2Scaler.ScaleAndFadeIn();
        await Task.Delay(500);
        row3Scaler.ScaleAndFadeIn();
        UIManager.Instance.bottomMenu.OnShowButtons();
    }

    private void CaculateGameScore()
    {
        Stopwatch.StopTimer();
        var totalSecond = Stopwatch.GetSeconds();
        clockText.text = totalSecond.ToString();
        scoreText.text = starText.text;
    }
}