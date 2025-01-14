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


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
        }

        private void OnDestroy()
        {
            DOTween.KillAll(); // Kills all active tweens
        }

        public void OnStartGame()
        {
            skillGenerator.DestroyAllSkills();
            missileGenerator.enabled = true;
            skillGenerator.enabled = true;
        }

        public void OnPlayAgain()
        {
            plane.SetActive(true);
            OnStartGame();
            uiManager.HideGameOverUI();
            skillGenerator.DestroyAllSkills();
        }

        public void OnGameOver()
        {
            missileGenerator.DestroyAllMissilesAndDeactivate();
            skillGenerator.enabled = false;
            uiManager.ShowGameOverPanel();
            skillGenerator.DestroyAllSkills();
        }

        public void OnAddScore(int pointValue)
        {
            UIManager.Instance.inGameUI.UpdateScore(pointValue);
        }
    }
}