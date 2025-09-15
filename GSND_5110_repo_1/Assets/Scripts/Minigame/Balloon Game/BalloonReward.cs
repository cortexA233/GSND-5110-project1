using UnityEngine;

namespace Minigame.Balloon_Game
{
    public class BallonReward
    {
        public class Balloon : MonoBehaviour
        {
            public int rewardPoints = 5;
            public float riseSpeed = 2f;

            void Update()
            {
                transform.Translate(Vector2.up * riseSpeed * Time.deltaTime);

                if (transform.position.y > Camera.main.orthographicSize + 1f)
                    Destroy(gameObject);
            }

            void OnMouseDown()
            {
                print("Reward");
            }
        }
    }
}