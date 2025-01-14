using System;
using DG.Tweening;
using UnityEngine;

namespace Transtions
{
    public class SlideTransition : MonoBehaviour
    {
        public float duration = 1f; // Duration of the slide
        public Ease slideEase = Ease.OutBack; // Ease for sliding       

        public Vector2 slideInTargetPosition; // Target position for SlideIn
        public Vector2 slideOutTargetPosition; // Target position for SlideIn

        private RectTransform rectTransform; // Object to slide

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnDisable()
        {
            rectTransform.DOKill();
        }

        /// <summary>
        ///     Slides the object to the target position.
        /// </summary>
        public void SlideIn()
        {
            rectTransform.DOAnchorPos(slideInTargetPosition, duration).SetEase(slideEase).SetUpdate(true)
                .SetAutoKill(true);
            // Animate to target position
        }

        /// <summary>
        ///     Slides the object out to just outside the screen.
        /// </summary>
        public void SlideOut(Action onComplete = null)
        {
            rectTransform.DOAnchorPos(slideOutTargetPosition, duration).SetEase(Ease.InBack)
                .SetUpdate(true).SetAutoKill(true).OnComplete(() => onComplete?.Invoke()); // Animate out
        }
    }
}