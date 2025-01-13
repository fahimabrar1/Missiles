using UnityEngine;
using UnityEngine.UI;
using Toggle = UnityEngine.UIElements.Toggle;

namespace UI
{
    public class ToggleButton : MonoBehaviour
    {
        public Image image;
        public Sprite activeSprite;
        public Sprite inactiveSprite;
        public Toggle toggle;

        public void OnChangeBool(bool value)
        {
            image.sprite = value ? activeSprite : inactiveSprite;
        }

        public void ToggleButtonOn(bool value)
        {
            toggle.value = value;
        }
    }
}