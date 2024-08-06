using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerPrefab;

    private int currentScore = 0;

    private Health playerHealth;
    private Health enemyHealth;

    private void Start()
    {
        // Ensure prefabs are assigned
        if (enemyPrefab == null || playerPrefab == null)
        {
            Debug.LogError("Prefabs are not assigned!");
            return;
        }

        playerHealth = playerPrefab.GetComponent<Health>();

        // Check if Health components are attached
        if (playerHealth != null)
        {
            playerHealth.OnTakeDamage += PlayerModifyScore;
        }
        else
        {
            Debug.LogError("Player prefab does not have a Health component.");
        }

        // Subscribe to enemies dynamically
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.OnEnemySpawned += OnEnemySpawned;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from health events to prevent memory leaks
        if (playerHealth != null)
        {
            playerHealth.OnTakeDamage -= PlayerModifyScore;
        }

        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.OnEnemySpawned -= OnEnemySpawned;
        }
    }

    private void OnEnemySpawned(GameObject enemy)
    {
        Health enemyHealth = enemy.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.OnTakeDamage += EnemyDamageModifyScore;
            enemyHealth.OnDeath += EnemyDeathModifyScore;
        }
        else
        {
            Debug.LogError("Spawned enemy does not have a Health component.");
        }
    }

    private void EnemyDeathModifyScore(object sender, HealthEventArgs e)
    {
        currentScore += e.ScorePoints;
    }

    private void EnemyDamageModifyScore(object sender, HealthEventArgs e)
    {
        currentScore += e.ScorePoints;
    }

    private void PlayerModifyScore(object sender, HealthEventArgs e)
    {
        currentScore -= e.ScorePoints;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
