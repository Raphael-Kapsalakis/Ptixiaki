using System.Collections.Generic;
using UnityEngine;

public class AdaptiveSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    public int baseEnemiesPerWave = 5;
    public float baseEnemySpeed = 3.5f;
    public float baseEnemyDamage = 10f;

    public int maxEnemiesPerWave = 50;
    public int minEnemiesPerWave = 2;

    public float maxEnemySpeed = 6f;
    public float minEnemySpeed = 2f;

    public float maxEnemyDamage = 25f;
    public float minEnemyDamage = 5f;

    private int currentEnemiesPerWave;
    private float currentEnemySpeed;
    private float currentEnemyDamage;

    private List<float> recentScores = new List<float>();

    void Start()
    {
        
    }

    public void InitializeSpawner()
    {
        currentEnemiesPerWave = baseEnemiesPerWave;
        currentEnemySpeed = baseEnemySpeed;
        currentEnemyDamage = baseEnemyDamage;
    }

    public void SpawnWave()
    {
        for (int i = 0; i < currentEnemiesPerWave; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

            ZombieAI ai = zombie.GetComponent<ZombieAI>();
            if (ai != null)
            {
                ai.waveManager = FindObjectOfType<WaveManager>();
                ai.speed = currentEnemySpeed;
                ai.damage = currentEnemyDamage;
            }
        }
    }

    public void RegisterPlayerPerformance(float score)
    {
        if (!AdaptiveDifficultyManager.Instance.adaptiveDifficultyEnabled)
            return;

        recentScores.Add(score);
        if (recentScores.Count > 3)
            recentScores.RemoveAt(0);

        AdjustDifficulty();
    }

    private void AdjustDifficulty()
    {
        if (recentScores.Count == 0) return;

        float weightedAverageScore = 0f;
        float weightSum = 0f;

        for (int i = 0; i < recentScores.Count; i++)
        {
            float weight = i + 1;
            weightedAverageScore += recentScores[i] * weight;
            weightSum += weight;
        }

        weightedAverageScore /= weightSum;

        // Adjust enemies per wave
        if (weightedAverageScore >= 60f)
            currentEnemiesPerWave = Mathf.Min(currentEnemiesPerWave + 3, maxEnemiesPerWave);
        else if (weightedAverageScore >= 30f)
            currentEnemiesPerWave = Mathf.Min(currentEnemiesPerWave + 1, maxEnemiesPerWave);
        else if (weightedAverageScore < 20f)
            currentEnemiesPerWave = Mathf.Max(currentEnemiesPerWave - 1, minEnemiesPerWave);

        // Adjust speed
        currentEnemySpeed = Mathf.Clamp(baseEnemySpeed + (weightedAverageScore * 0.05f), minEnemySpeed, maxEnemySpeed);

        // Adjust damage
        currentEnemyDamage = Mathf.Clamp(baseEnemyDamage + (weightedAverageScore * 0.1f), minEnemyDamage, maxEnemyDamage);

        Debug.Log($"[AdaptiveSpawner] Enemies: {currentEnemiesPerWave} | Speed: {currentEnemySpeed:F1} | Damage: {currentEnemyDamage:F1} | Avg Score: {weightedAverageScore:F1}");
    }

    public int GetCurrentEnemiesPerWave() => currentEnemiesPerWave;
}

