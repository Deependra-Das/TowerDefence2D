using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tower
{
    public TowerConfig.TowerTypes towerType;
    public GameObject towerPrefab;
    public int buildCost;
}
