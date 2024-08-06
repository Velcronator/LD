using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 3f;
    private WaveConfigSO currentWave;

    private bool isLooping = true;

    public event Action<GameObject> OnEnemySpawned;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    private IEnumerator SpawnEnemyWaves()
    {
        System.Random random = new System.Random();
        int waveIndex = random.Next(0, waveConfigs.Count);
        do
        {
            currentWave = waveConfigs[waveIndex];
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                GameObject enemy = Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.identity, transform);
                OnEnemySpawned?.Invoke(enemy); // Notify subscribers
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
            waveIndex = random.Next(0, waveConfigs.Count);
        } while (isLooping);
    }

}
