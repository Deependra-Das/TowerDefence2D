using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWaveSystem
{
    public int enemyWaveIndex;
    public int numberOfEnemies = 3;
    public float enemyFrequency = 0.5f;
    public EnemyConfig.EnemyType[] enemySpawnOrder;
}
