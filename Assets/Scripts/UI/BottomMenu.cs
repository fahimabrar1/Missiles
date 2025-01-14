using Transtions;
using UnityEngine;

namespace UI
{
    public class BottomMenu : MonoBehaviour
    {
        public ScaleAndFade homesButtonScaler;
        public ScaleAndFade settingsPanel;
        public SlideTransition bottomMenuSlider;
        public GameObject homeButtonObj;

        public void OnClicHomeButton()
        {
            UIManager.Instance.OnReturnToMainMenu();
        }

        public void OnClicSettingsButton()
        {
            settingsPanel.ScaleAndFadeIn();
        }

        public void OnClicSettingsClose()
        {
            settingsPanel.ScaleAndFadeOut();
        }

        public void OnHideButtons()
        {
            bottomMenuSlider.SlideOut(() => homeButtonObj.SetActive(false));
        }

        public void OnShowSettings()
        {
            homeButtonObj.SetActive(false);
            settingsPanel.ScaleAndFadeIn();
        }


        public void OnShowButtons()
        {
            homeButtonObj.SetActive(true);
            bottomMenuSlider.SlideIn();
        }

        public void OnShowSettingsButtons()
        {
            homeButtonObj.SetActive(false);
            bottomMenuSlider.SlideIn();
        }
    }
}