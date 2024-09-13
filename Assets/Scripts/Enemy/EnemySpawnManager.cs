using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameUIManager gameUIManagerObj;

    [SerializeField]
    private float coolDownBetweenWaves = 5.0f;

    private int currentWave = 0;
    private float timeSinceLastEnemySpawned;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning =false;
    private int spawnCounter=0;
    private EnemyConfig.EnemyType[] enemySpawnOrder;

    private List<Transform> enemies;

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private EnemyWaveSystem[] enemyWaves;

    private static EnemySpawnManager instance;
    public static EnemySpawnManager Instance { get { return instance; } }

    public UnityEvent onEnemyDestroy = new UnityEvent();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        onEnemyDestroy.AddListener(EnemyDestroyed);
        enemies = new List<Transform>();
    }

    public void StartSpawner()
    {
        currentWave = 0;
        isSpawning = false;
        StartCoroutine(StartWave());
    }

    void Update()
    {
        if (!isSpawning) return;

        timeSinceLastEnemySpawned += Time.deltaTime;

        if(timeSinceLastEnemySpawned>=(1f/ getEnemyWaveSystem(currentWave).enemyFrequency) && enemiesLeftToSpawn >0)
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
        enemies = new List<Transform>();
        int waveNumber = currentWave + 1;
        gameUIManagerObj.SetWaveText("Wave " + waveNumber);
        gameUIManagerObj.SetLabelText("Starts in");
        gameUIManagerObj.ShowTimerText(coolDownBetweenWaves);
        yield return new WaitForSeconds(coolDownBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = getEnemyWaveSystem((int)currentWave).numberOfEnemies;
        spawnCounter = 0;
        enemySpawnOrder= getEnemyWaveSystem((int)currentWave).enemySpawnOrder;
        gameUIManagerObj.SetLabelText("ONGOING");
        gameUIManagerObj.HideTimerText();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastEnemySpawned = 0f;
        if(currentWave< enemyWaves.Length-1 && GameManager.Instance.isGameOver==false)
        {
            currentWave++;
            StartCoroutine(StartWave());
        }
        else
        {
            GameManager.Instance.OnGameCompleted();
        }
      
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn=null;
        GameObject newEnemy;

        switch (enemySpawnOrder[spawnCounter])
        {
            case EnemyConfig.EnemyType.SMALL:
                prefabToSpawn = enemyPrefabs[0];
                break;
            case EnemyConfig.EnemyType.MEDIUM:
                prefabToSpawn = enemyPrefabs[1];
                break;
            case EnemyConfig.EnemyType.TANK:
                prefabToSpawn = enemyPrefabs[2];
                break;
        }
        if (prefabToSpawn != null)
        {
            newEnemy = Instantiate(prefabToSpawn, GameManager.Instance.startPoint.position, Quaternion.identity);
            enemies.Add(newEnemy.transform);
            spawnCounter++;
        }
    }

    private EnemyWaveSystem getEnemyWaveSystem(int waveIndex)
    {
        EnemyWaveSystem waveItem = Array.Find(enemyWaves, item => item.enemyWaveIndex == waveIndex);
        if (waveItem != null)
        {
            return waveItem;
        }
        return null;
    }

    public void DestroyAllEnemiesOnScreen()
    {
        if(enemies.Count>0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i]!=null)
                {
                    EnemyDestroyed();
                    Destroy(enemies[i].transform.gameObject);
                }
            
            }
        }
       
    }


}