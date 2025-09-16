using Minigame.BallonGame;
using UnityEngine;

namespace Minigame.BalloonGame
{
    public class Bomb : MonoBehaviour
    {
        public float riseSpeed = 2f;
        public int lifePenalty = 1;

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

            BalloonGameManager.Instance.LoseLife(lifePenalty);
            Destroy(gameObject);
        }
    }
}