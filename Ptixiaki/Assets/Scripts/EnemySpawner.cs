using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int spawnAmount = 1;
    public float spawnDelay = 0.5f;
    public bool spawnOnStart = true;

    private bool hasSpawned = false;

    void Start()
    {
        if (spawnOnStart)
        {
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        if (enemyPrefab != null && !hasSpawned)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 spawnPos = transform.position;

                // Optional slight position offset to avoid perfect overlap
                spawnPos += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

                Instantiate(enemyPrefab, spawnPos, transform.rotation);
            }

            hasSpawned = true;
        }
    }
}
