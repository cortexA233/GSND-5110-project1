using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public GameObject bombPrefab;
    public float spawnInterval = 1.2f;
    public float xRange = 8f;
    [Range(0f,1f)] public float bombChance = 0.3f;

    bool spawning = true;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 1f, spawnInterval);
    }

    void SpawnObject()
    {
        if (!spawning) return;

        float xPos = Random.Range(-xRange, xRange);
        Vector3 spawnPos = new Vector3(xPos, -Camera.main.orthographicSize - 1f, 0);

        GameObject prefabToSpawn =
            (Random.value < bombChance) ? bombPrefab : balloonPrefab;

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}