using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Difficulty {
    Easy,
    Medium,
    Hard
} 
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Transform> spawnPoints;

    [Header("Spawn Settings")] 
    [SerializeField] private List<float> difficultyTimers;
    [SerializeField] private List<float> spawnRates;

    private float _spawnRate = 10f;
    private float _nextSpawnTime = 0;
    void Update()
    {
        if (Time.timeSinceLevelLoad > difficultyTimers[(int)Difficulty.Easy])
        {
            var spawnRate = spawnRates[(int)Difficulty.Easy];
        } else if (Time.timeSinceLevelLoad > difficultyTimers[(int)Difficulty.Medium])
        {
            var spawnRate = spawnRates[(int)Difficulty.Medium];
        } else if (Time.timeSinceLevelLoad > difficultyTimers[(int)Difficulty.Hard])
        {
            var spawnRate = spawnRates[(int)Difficulty.Hard];
        }

        if (_nextSpawnTime <= Time.timeSinceLevelLoad)
        {
            _nextSpawnTime = Time.timeSinceLevelLoad + _spawnRate;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], 
            spawnPoints[Random.Range(0, spawnPoints.Count)].position, 
            Quaternion.identity);
    }
}
