using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public MissileGenerator missileGenerator; // Reference to the MissileGenerator


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void OnStartGame()
        {
            missileGenerator.enabled = true;
        }
    }
}