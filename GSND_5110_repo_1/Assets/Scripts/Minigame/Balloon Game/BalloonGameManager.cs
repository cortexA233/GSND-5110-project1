using TMPro;
using UnityEngine;

namespace Minigame.Balloon_Game
{
    public class BalloonGameManager : MonoBehaviour
    {
        public static BalloonGameManager Instance;
    
        public TMP_Text timerText;
        public float popTime = 20f;

        private int score;
        float timeLeft;
        bool gameOver = false;

        void Awake()
        {
            if (Instance == null) Instance = this;
        }

        void Start()
        {
            timeLeft = popTime;
        }

        void Update()
        {
            if (gameOver) return;
        
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = 0f;
                gameOver = true;
            }

            timerText.text = $"Time: {timeLeft:F1}";
        }

        void UpdateUI()
        {
            print("!!!!");
        }
    
        void EndGame()
        {
            gameOver = true;
            // stop new spawns
            BalloonSpawner spawner = FindObjectOfType<BalloonSpawner>();
            if (spawner) spawner.StopSpawning();

            // show a Game Over screen or restart
            Debug.Log($"Game Over! Final Score: {score}");
        }
    }
}
