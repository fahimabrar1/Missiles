using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Indicator
{
    public class MissileIndicator : MonoBehaviour, IMissileIndicator
    {
        private Transform _target;
        private Camera _mainCamera;
        private RectTransform _indicator;
        private Image image;
        [SerializeField]
        private float screenOffset = 50f; // Offset distance from the screen edges


        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void Initialize(Transform target, Camera mainCamera)
        {
            _target = target;
            _mainCamera = mainCamera;
            _indicator = GetComponent<RectTransform>();
        }

        public void OnDestroyMissile()
        {
            Destroy(gameObject);
        }

        private void LateUpdate()
        {
            if (_target is null)
            {
                Destroy(gameObject); // Destroy the indicator if the missile is gone
                return;
            }

            // Convert target position to screen space
            var screenPoint = _mainCamera.WorldToScreenPoint(_target.position);
            var isOnScreen = screenPoint.z > 0 &&
                             screenPoint.x > 0 && screenPoint.x < Screen.width &&
                             screenPoint.y > 0 && screenPoint.y < Screen.height;

            // Show/hide indicator
            image.enabled =!isOnScreen;

            if (isOnScreen) return; // Clamp the indicator to the edges of the screen with offset
            screenPoint.x = Mathf.Clamp(screenPoint.x, screenOffset, Screen.width - screenOffset);
            screenPoint.y = Mathf.Clamp(screenPoint.y, screenOffset, Screen.height - screenOffset);
            _indicator.position = screenPoint;

            // Rotate the indicator towards the target's direction
            var direction = screenPoint - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _indicator.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}