using DG.Tweening;
using UnityEngine;

namespace Transtions
{
    public class SlideTransition : MonoBehaviour
    {
        public enum SlideDirection
        {
            Top,
            Down,
            Left,
            Right
        }

        public SlideDirection direction; // Direction to slide
        public float duration = 1f; // Duration of the slide
        public Ease slideEase = Ease.OutBack; // Ease for sliding

        private RectTransform rectTransform; // Object to slide

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        ///     Slides the object in from just outside the screen.
        /// </summary>
        public void SlideIn()
        {
            var originalPosition = rectTransform.anchoredPosition;
            var startPosition = GetOutsidePosition();

            rectTransform.anchoredPosition = startPosition; // Start from outside the screen
            rectTransform.DOAnchorPos(originalPosition, duration).SetEase(slideEase); // Animate to original position
        }

        /// <summary>
        ///     Slides the object out to just outside the screen.
        /// </summary>
        public void SlideOut()
        {
            var targetPosition = GetOutsidePosition();

            rectTransform.DOAnchorPos(targetPosition, duration).SetEase(Ease.InBack)
                .OnComplete(() => gameObject.SetActive(false)); // Deactivate after sliding out
        }

        /// <summary>
        ///     Calculates the position just outside the screen based on the slide direction.
        /// </summary>
        /// <returns>Position outside the screen.</returns>
        private Vector2 GetOutsidePosition()
        {
            var outsidePosition = rectTransform.anchoredPosition;
            var rectSize = rectTransform.rect.size;

            switch (direction)
            {
                case SlideDirection.Top:
                    outsidePosition.y += rectSize.y * (1 - rectTransform.pivot.y); // Above the screen
                    break;
                case SlideDirection.Down:
                    outsidePosition.y -= rectSize.y * rectTransform.pivot.y; // Below the screen
                    break;
                case SlideDirection.Left:
                    outsidePosition.x -= rectSize.x * rectTransform.pivot.x; // Left of the screen
                    break;
                case SlideDirection.Right:
                    outsidePosition.x += rectSize.x * (1 - rectTransform.pivot.x); // Right of the screen
                    break;
            }

            return outsidePosition;
        }
    }
}