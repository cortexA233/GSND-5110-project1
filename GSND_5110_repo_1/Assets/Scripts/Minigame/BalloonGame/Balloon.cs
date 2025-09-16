using Minigame.BallonGame;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public int rewardPoints = 5;
    public float riseSpeed = 2f;

    void Update()
    {
        if (BalloonGameManager.Instance != null && BalloonGameManager.Instance.IsGameOver) return;

        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);

        float top = Camera.main.transform.position.y + Camera.main.orthographicSize + 1f;
        if (transform.position.y > top) Destroy(gameObject);
    }

    void OnMouseDown()
    {
        if (BalloonGameManager.Instance != null && BalloonGameManager.Instance.IsGameOver) return;

        BalloonGameManager.Instance.AddScore(rewardPoints);
        Destroy(gameObject);
    }
}
