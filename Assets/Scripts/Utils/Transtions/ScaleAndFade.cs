using DG.Tweening;
using UnityEngine;

namespace Transtions
{
    public class ScaleAndFade : MonoBehaviour
    {
        public CanvasGroup canvasGroup; // Optional: For fading
        public float duration = 1f; // Duration for scaling and fading
        public float scaleSize = 1; // Scale size
        public Ease scaleEase = Ease.OutElastic; // Ease for scaling
        public Ease fadeEase = Ease.InOutSine; // Ease for fading

        /// <summary>
        ///     Scales up and fades in the object.
        /// </summary>
        public void ScaleAndFadeIn()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1, duration).SetEase(fadeEase);
            }

            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, duration).SetEase(scaleEase);
        }

        /// <summary>
        ///     Scales down and fades out the object.
        /// </summary>
        public void ScaleAndFadeOut()
        {
            if (canvasGroup != null) canvasGroup.DOFade(0, duration).SetEase(fadeEase);

            transform.DOScale(Vector3.zero, duration).SetEase(Ease.InBack)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}