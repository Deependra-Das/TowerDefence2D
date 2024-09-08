using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private static TowerManager instance;
    public static TowerManager Instance { get { return instance; } }

    [SerializeField]
    private Tower[] towerList;

    private TowerTypes selectedTower;

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
    }

    private void Start()
    {
        selectedTower = TowerTypes.BASIC;
    }

    public Tower GetTower(TowerTypes type)
    {
        Tower towerItem = Array.Find(towerList, item => item.towerType == type);
        if (towerItem != null)
        {
            return towerItem;
        }
        return null;
    }

    public GameObject GetSelectedTower()
    {
        return GetTower(selectedTower).towerPrefab;
    }

}

public enum TowerTypes
{
    BASIC,
    HEAVY,
    FREEZE,
}

[Serializable]
public class Tower
{
    public TowerTypes towerType;
    public GameObject towerPrefab;
    public int buildCost;
}
