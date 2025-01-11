using DG.Tweening;
using Transtions;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public ScaleAndFade resumeButtonScaler;
    public ScaleAndFade pauseButtonScaler;
    public SlideTransition headerSlider;

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
}