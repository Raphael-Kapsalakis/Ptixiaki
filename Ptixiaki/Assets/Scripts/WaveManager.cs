
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public AdaptiveSpawner spawner;
    public int totalWaves = 10;
    public int waveNumber = 1;

    private int killsThisWave = 0;
    private float waveStartTime;
    private int enemiesToKill;

    void Start()
    {
        spawner.InitializeSpawner();
        StartNewWave();
    }

    public void RegisterKill()
    {
        killsThisWave++;
        if (killsThisWave >= enemiesToKill)
        {
            EndWave();
        }
    }

    public void StartNewWave()
    {
        if (waveNumber > totalWaves)
        {
            Debug.Log("Game Over — You Survived!");
            return;
        }

        Debug.Log($"Wave {waveNumber} Starting...");
        killsThisWave = 0;
        waveStartTime = Time.time;

        enemiesToKill = spawner.GetCurrentEnemiesPerWave();
        spawner.SpawnWave();

        Debug.Log($"Enemies This Wave: {enemiesToKill}");
    }

    public void EndWave()
    {
        float survivalTime = Time.time - waveStartTime;
        float performanceScore = (killsThisWave * 5f) + survivalTime;

        Debug.Log($"Wave {waveNumber} Complete | Kills: {killsThisWave} | Time: {survivalTime:F1}s | Score: {performanceScore:F1}");

        spawner.RegisterPlayerPerformance(performanceScore);

        waveNumber++;
        StartNewWave();
    }
}


