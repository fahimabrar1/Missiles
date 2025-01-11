using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public UIManager uiManager; // Reference to the UIManager
        public MissileGenerator missileGenerator; // Reference to the MissileGenerator
        public SkillGenerator skillGenerator; // Reference to the MissileGenerator
        public GameObject plane;

        public int score;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            DOTween.KillAll(); // Kills all active tweens
        }

        public void OnStartGame()
        {
            score = 0;
            missileGenerator.enabled = true;
            skillGenerator.enabled = true;
        }

        public void OnPlayAgain()
        {
            plane.SetActive(true);
            OnStartGame();
            uiManager.HideGameOverUI();
        }

        public void OnGameOver()
        {
            missileGenerator.enabled = false;
            skillGenerator.enabled = false;
            uiManager.ShowGameOverPanel();
        }

        public void OnAddScore(int pointValue)
        {
            score += pointValue;
        }
    }
}