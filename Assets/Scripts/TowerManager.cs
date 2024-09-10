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

    private TowerTypes selectedTower= TowerTypes.NONE;

    private List<Transform> turrets;

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
        turrets = new List<Transform>();
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

    public Tower GetSelectedTower()
    {
        return GetTower(selectedTower);
    }

    public void SetSelectedTower(TowerTypes type)
    {
        switch(type)
        {
            case TowerTypes.BASIC:
                selectedTower = TowerTypes.BASIC;
                break;

            case TowerTypes.HEAVY:
                selectedTower = TowerTypes.HEAVY;
                break;

            case TowerTypes.FREEZE:
                selectedTower = TowerTypes.FREEZE;
                break;

        }

    }

    public void AddTurretToList(GameObject towerObj)
    {
        turrets.Add(towerObj.transform);
    }

    public void DestroyAllTurretsOnScreen()
    {
        if (turrets.Count > 0)
        {
            for (int i = 0; i < turrets.Count; i++)
            {
                if (turrets[i] != null)
                {
                    Destroy(turrets[i].transform.gameObject);
                }

            }
        }

        turrets=new List<Transform> ();

    }
}

public enum TowerTypes
{
    BASIC,
    HEAVY,
    FREEZE,
    NONE,
}

[Serializable]
public class Tower
{
    public TowerTypes towerType;
    public GameObject towerPrefab;
    public int buildCost;
}
