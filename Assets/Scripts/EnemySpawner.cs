using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;
    WaveConfigSO currentWave;
    int totalWavesSpawned = 0;

    void Start()
    {
        if (waveConfigs == null || waveConfigs.Count == 0)
        {
            Debug.LogError("Wave configurations are not set.");
            return;
        }
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            currentWave = GetWeightedRandomWave();
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(currentWave.GetEnemyPrefab(i),
                            currentWave.GetStartingWaypoint().position,
                            Quaternion.Euler(0, 0, 180),
                            transform);
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
            totalWavesSpawned++;
        }
        while (isLooping);
    }

    WaveConfigSO GetWeightedRandomWave()
    {
        int waveCount = waveConfigs.Count;
        float[] weights = new float[waveCount];
        float totalWeight = 0f;

        for (int i = 0; i < waveCount; i++)
        {
            // Increase weight for later waves as more waves are spawned
            weights[i] = Mathf.Pow(i + 1, totalWavesSpawned + 1);
            totalWeight += weights[i];
        }

        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        for (int i = 0; i < waveCount; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                return waveConfigs[i];
            }
        }

        return waveConfigs[waveCount - 1]; // Fallback in case of rounding errors
    }
}
