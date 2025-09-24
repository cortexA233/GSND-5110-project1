
namespace Minigame.BalloonGame
{
    using UnityEngine;
    using KToolkit;

    public class BalloonSpawner : MonoBehaviour
    {
        public GameObject balloonPrefab;
        public GameObject bombPrefab;
        public float spawnInterval = 0.8f;
        [Range(0f,1f)] public float bombChance = 0.25f;
        public float spawnXRange = 8f;    // set in inspector
        public float spawnXOffset = 5f;
        public float spawnYOffset = 1f;

        bool spawning = true;

        void Start()
        {
            InvokeRepeating(nameof(SpawnObject), 0.5f, spawnInterval);
        }

        void SpawnObject()
        {
            if (!spawning) return;

            float xPos = Random.Range(-spawnXRange + spawnXOffset, spawnXRange + spawnXOffset);
            KDebugLogger.Cortex_DebugLog(xPos);
            float y = Camera.main.transform.position.y - Camera.main.orthographicSize - spawnYOffset;
            Vector3 spawnPos = new Vector3(xPos, y, 0f);

            GameObject prefab = (Random.value < bombChance) ? bombPrefab : balloonPrefab;
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }

        public void StopSpawning()
        {
            spawning = false;
            CancelInvoke(nameof(SpawnObject));
        }
    }
}