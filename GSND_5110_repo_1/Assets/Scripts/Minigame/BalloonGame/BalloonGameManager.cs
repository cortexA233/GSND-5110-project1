using Minigame.BalloonGame;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using KToolkit;


namespace Minigame.BallonGame
{
    public class BalloonGameManager : MonoBehaviour
    {
        public static BalloonGameManager Instance;

        [Header("UI")] public TMP_Text scoreText;
        public TMP_Text livesText;
        public TMP_Text timerText;
        public GameObject gameOverUI; // optional: panel to show end results

        [Header("Game settings")] public int startingLives = 2;
        // just give default value
        public float roundTime = 20f;
        public string returnSceneName = "MainPuzzle";

        int score;
        int lives;
        float timeLeft;
        bool gameOver = false;

        // expose to other scripts
        public bool IsGameOver => gameOver;

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        void Start()
        {
            score = 0;
            lives = startingLives;
            timeLeft = roundTime;
            UpdateUI();
            // get the round time from game config
            roundTime = GameManager.instance.GetGameConfig().minigameCountdownTime;
            if (gameOverUI) gameOverUI.SetActive(false);
        }

        void Update()
        {
            if (gameOver) return;

            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0f)
            {
                timeLeft = 0f;
                EndGame();
            }

            if (timerText) timerText.text = $"Time: {timeLeft:F1}";
        }

        void UpdateUI()
        {
            if (scoreText) scoreText.text = $"Score: {score}";
            if (livesText) livesText.text = $"Lives: {lives}";
        }

        public void AddScore(int amount)
        {
            if (gameOver) return;
            score += amount;
            UpdateUI();
        }

        public void LoseLife(int amount = 1)
        {
            if (gameOver) return;
            lives -= amount;
            if (lives < 0) lives = 0;
            UpdateUI();
            if (lives <= 0) EndGame();
        }

        void EndGame()
        {
            gameOver = true;

            // stop spawner
            var spawner = FindObjectOfType<BalloonSpawner>();
            if (spawner) spawner.StopSpawning();

            // destroy leftover objects (optional)
            foreach (var b in FindObjectsOfType<Balloon>()) Destroy(b.gameObject);
            foreach (var b in FindObjectsOfType<Bomb>()) Destroy(b.gameObject);

            // store result to be used by main puzzle
            GameResults.balloonScore = score;

            // show Game Over UI if set
            if (gameOverUI) gameOverUI.SetActive(true);
            
            // show the main puzzle
            // KEventManager.SendNotification(KEventName.ShowMainPuzzle, true);
            
            // different results for success and failure
            if (lives <= 0 || score < GameManager.instance.GetGameConfig().minigameMinimalScore)
            {
                // deduct the time for penalty
                CountDownManager.instance.ChangeCountDownTime(GameManager.instance.GetGameConfig().minigamePenaltyTime);
                KUIManager.instance.CreateUI<HintUI>("YOU LOST THE MINIGAME!", 3f);
            }
            else
            {
                // calculate the score and add extra time
                CountDownManager.instance.ChangeCountDownTime(score * GameManager.instance.GetGameConfig()
                    .scoreToExtraTimeRatio);
                KUIManager.instance.CreateUI<HintUI>("YOU'VE GOT EXTRA TIME!", 3f);
            }
            
            // destroy the root prefab
            GameObject.Destroy(transform.parent.gameObject);
        }

        // Hook this to GameOver button to return to main puzzle
        public void ReturnToMainPuzzle()
        {
            SceneManager.LoadScene(returnSceneName);
        }
    }
}
