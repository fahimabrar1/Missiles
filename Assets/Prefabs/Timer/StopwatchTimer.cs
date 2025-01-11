using TMPro;
using UnityEngine;

namespace Prefabs.Timer
{
    public class StopwatchTimer : MonoBehaviour
    {
        public TMP_Text timerText; // Reference to a UI Text component

        private float _elapsedTime; // Tracks the time elapsed
        private bool _timerRunning; // Controls whether the timer is running

        private void Update()
        {
            if (!_timerRunning) return;
            // Increment the elapsed time
            _elapsedTime += Time.deltaTime;

            // Update the timer display
            UpdateTimerDisplay();
        }

        public void OnEnable()
        {
            StartTimer();
        }

        private void UpdateTimerDisplay()
        {
            var minutes = Mathf.FloorToInt(_elapsedTime / 60);
            var seconds = Mathf.FloorToInt(_elapsedTime % 60);
            if (minutes > 9)
                timerText.text = $"{minutes:00}:{seconds:00}";
            else
                timerText.text = $"{minutes:0}:{seconds:00}";
        }

        public void StartTimer()
        {
            _timerRunning = true;
        }

        public void StopTimer()
        {
            _timerRunning = false;
        }

        public void ResetTimer()
        {
            _elapsedTime = 0f;
            UpdateTimerDisplay();
        }
    }
}