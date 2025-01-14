using System;
using DG.Tweening;
using UnityEngine;

namespace Transtions
{
    public class ScaleAndFade : MonoBehaviour
    {
        public CanvasGroup canvasGroup; // Optional: For fading
        public float duration = 1f; // Duration for scaling and fading
        public float fadeOutSaleSize = 1; // Scale size
        public Ease scaleEase = Ease.OutElastic; // Ease for scaling
        public Ease fadeEase = Ease.InOutSine; // Ease for fading

        private void OnDisable()
        {
            transform.DOKill();
            if (canvasGroup != null) DOTween.Kill(canvasGroup);
        }

        /// <summary>
        ///     Scales up and fades in the object.
        /// </summary>
        public void ScaleAndFadeIn()
        {
            gameObject.SetActive(true);
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1, duration).SetEase(fadeEase).SetUpdate(true).SetAutoKill(true);
            }

            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, duration).SetEase(scaleEase).SetUpdate(true).SetAutoKill(true);
        }

        /// <summary>
        ///     Scales down and fades out the object.
        /// </summary>
        public void ScaleAndFadeOut(Action onComplete = null)
        {
            if (canvasGroup != null)
                canvasGroup.DOFade(0, duration).SetEase(fadeEase).SetUpdate(true).SetAutoKill(true);


            transform.DOScale(new Vector3(fadeOutSaleSize, fadeOutSaleSize, fadeOutSaleSize), duration)
                .SetEase(scaleEase).SetUpdate(true).SetAutoKill(true)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    transform.localScale = Vector3.zero;
                    onComplete?.Invoke();
                });
        }
    }
}