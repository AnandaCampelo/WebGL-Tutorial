using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float initialSpawnInterval = 5f;
    public float minSpawnInterval = 0.8f;
    public float lifetime = 5f;
    public Tilemap groundTilemap;
    public float minDistanceFromPlayer = 1.5f;

    private float nextSpawnTime = 0f;
    private Transform playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTransform = player.transform;
    }

    private void Update()
    {
        float time = GameManager.timeSurvived;
        float spawnInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - time / 30f);

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos;
        int maxAttempts = 15;
        int attempts = 0;
        float checkRadius = 0.3f;

        Bounds bounds = groundTilemap.localBounds;

        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            spawnPos = new Vector3(x, y, 0f);

            Vector3Int cell = groundTilemap.WorldToCell(spawnPos);
            bool hasTile = groundTilemap.GetTile(cell) != null;
            bool notOverlapping = Physics2D.OverlapCircle(spawnPos, checkRadius) == null;
            bool notTooCloseToPlayer = playerTransform == null || Vector3.Distance(spawnPos, playerTransform.position) > minDistanceFromPlayer;

            if (hasTile && notOverlapping && notTooCloseToPlayer)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                Destroy(enemy, lifetime);
                return;
            }

            attempts++;
        }
        while (attempts < maxAttempts);
    }
}
