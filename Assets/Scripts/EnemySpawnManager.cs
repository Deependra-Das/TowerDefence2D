using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private float coolDownBetweenWaves = 5.0f;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 0;
    private float timeSinceLastEnemySpawned;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning =false;

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private EnemyWaveSystem[] enemyWaves;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    void Start()
    {
        StartCoroutine(StartWave());
    }

    void Update()
    {
        if (!isSpawning) return;

        timeSinceLastEnemySpawned += Time.deltaTime;

        if(timeSinceLastEnemySpawned>=(1f/ getEnemyWaveSystem((EnemyWaveIndex)currentWave).enemyFrequency) && enemiesLeftToSpawn >0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastEnemySpawned = 0f;
        }
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(coolDownBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = getEnemyWaveSystem((EnemyWaveIndex)currentWave).numberOfEnemies;
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastEnemySpawned = 0f;
        if(currentWave<2)
        {
            currentWave++;
            StartCoroutine(StartWave());
        }
        else
        {
            Debug.Log("Game Completed");
        }
      
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, GameManager.Instance.startPoint.position, Quaternion.identity);
    }

    private EnemyWaveSystem getEnemyWaveSystem(EnemyWaveIndex waveIndex)
    {
        EnemyWaveSystem waveItem = Array.Find(enemyWaves, item => item.enemyWaveIndex == waveIndex);
        if (waveItem != null)
        {
            return waveItem;
        }
        return null;
    }


}

public enum EnemyWaveIndex
{
    Wave1,
    Wave2,
    Wave3,
}

[Serializable]
public class EnemyWaveSystem
{
    public EnemyWaveIndex enemyWaveIndex;
    public int numberOfEnemies = 8;
    public float enemyFrequency = 0.5f;
}