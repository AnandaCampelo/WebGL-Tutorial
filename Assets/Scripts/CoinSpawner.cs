using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 7.0f;
    public float spawnRadius = 5.0f;
    public Tilemap groundTilemap;

    private void Start()
    {
        InvokeRepeating("SpawnCoin", 0f, spawnInterval);
    }

    private void SpawnCoin()
    {
        Vector3 randomPosition;
        int maxAttempts = 20;
        int attempts = 0;
        float checkRadius = 0.3f;

        Bounds tilemapBounds = groundTilemap.localBounds;

        do
        {
            float x = Random.Range(tilemapBounds.min.x, tilemapBounds.max.x);
            float y = Random.Range(tilemapBounds.min.y, tilemapBounds.max.y);
            randomPosition = new Vector3(x, y, transform.position.z);

            Vector3Int cellPosition = groundTilemap.WorldToCell(randomPosition);
            TileBase tile = groundTilemap.GetTile(cellPosition);

            bool validTile = tile != null;
            bool notOverlapping = Physics2D.OverlapCircle(randomPosition, checkRadius) == null;

            if (validTile && notOverlapping)
            {
                Instantiate(coinPrefab, randomPosition, Quaternion.identity);
                return;
            }

            attempts++;
        }
        while (attempts < maxAttempts);
    }
}
