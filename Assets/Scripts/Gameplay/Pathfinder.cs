using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private float randomRange = 5f; // Range for randomizing waypoint positions

    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        RandomizeMiddleWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void RandomizeMiddleWaypoints()
    {
        // Extract middle waypoints
        List<Transform> middleWaypoints = waypoints.GetRange(1, waypoints.Count - 2);

        // Shuffle middle waypoints
        for (int i = 0; i < middleWaypoints.Count; i++)
        {
            Transform temp = middleWaypoints[i];
            int randomIndex = Random.Range(i, middleWaypoints.Count);
            middleWaypoints[i] = middleWaypoints[randomIndex];
            middleWaypoints[randomIndex] = temp;
        }

        // Randomize positions within the specified range and restrict them to the screen
        foreach (Transform waypoint in middleWaypoints)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange), 0);
            Vector3 newPosition = waypoint.position + randomOffset;
            newPosition.x = Mathf.Clamp(newPosition.x, ScreenBounds.MinX, ScreenBounds.MaxX);
            newPosition.y = Mathf.Clamp(newPosition.y, ScreenBounds.MinY, ScreenBounds.MaxY);
            waypoint.position = newPosition;
        }

        // Reinsert middle waypoints back into the main list
        waypoints.RemoveRange(1, waypoints.Count - 2);
        waypoints.InsertRange(1, middleWaypoints);
    }
}
