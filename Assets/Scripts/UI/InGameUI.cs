using Transtions;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public ScaleAndFade resumeButtonScaler;
    public SlideTransition headerSlider;

    private void OnEnable()
    {
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
        resumeButtonScaler.gameObject.SetActive(true);
        resumeButtonScaler.ScaleAndFadeIn();
    }

    public void OnResumeGame()
    {
        resumeButtonScaler.gameObject.SetActive(false);
        resumeButtonScaler.ScaleAndFadeOut();
    }
}