using Transtions;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public ScaleAndFade resumeButtonScaler;
    public SlideTransition headerSlider;

    private void OnEnable()
    {
        headerSlider.SlideIn();
        resumeButtonScaler.transform.localScale = Vector3.zero;
        resumeButtonScaler.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
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