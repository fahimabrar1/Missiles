using Transtions;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public ScaleAndFade resumeButtonScaler;

    public ScaleAndFade pauseButtonScaler;
    public SlideTransition headerSlider;

    private void OnEnable()
    {
        pauseButtonScaler.ScaleAndFadeIn();
        resumeButtonScaler.transform.localScale = Vector3.zero;
        resumeButtonScaler.gameObject.SetActive(false);
        headerSlider.gameObject.SetActive(true);
        headerSlider.SlideIn();
    }

    private void OnDisable()
    {
        headerSlider.gameObject.SetActive(false);
        headerSlider.SlideOut();
        resumeButtonScaler.ScaleAndFadeOut();
    }


    public void OnPauseGame()
    {
        Time.timeScale = 0;
        pauseButtonScaler.ScaleAndFadeOut();

        resumeButtonScaler.gameObject.SetActive(true);
        resumeButtonScaler.ScaleAndFadeIn();
    }

    public void OnResumeGame()
    {
        Time.timeScale = 1;
        pauseButtonScaler.gameObject.SetActive(true);
        pauseButtonScaler.ScaleAndFadeIn();
        resumeButtonScaler.ScaleAndFadeOut();
    }
}