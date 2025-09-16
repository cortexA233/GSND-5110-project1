using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text timerText;   // <- Drag a UI text here
    public int startingLives = 3;
    public float roundTime = 20f; // <- 20-second game

    int score;
    int lives;
    float timeLeft;
    bool gameOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        score = 0;
        lives = startingLives;
        timeLeft = roundTime;
        UpdateUI();
    }

    void Update()
    {
        if (gameOver) return;

        // Countdown
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            EndGame();
        }

        timerText.text = $"Time: {timeLeft:F1}";
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        livesText.text = $"Lives: {lives}";
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
        UpdateUI();
        if (lives <= 0) EndGame();
    }

    void EndGame()
    {
        gameOver = true;
        // stop new spawns
        Spawner spawner = FindObjectOfType<Spawner>();
        if (spawner) spawner.StopSpawning();

        // show a Game Over screen or restart
        Debug.Log($"Game Over! Final Score: {score}");
    }
}