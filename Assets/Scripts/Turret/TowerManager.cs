using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private static TowerManager instance;
    public static TowerManager Instance { get { return instance; } }

    [SerializeField]
    private Tower[] towers;

    private TowerConfig.TowerTypes selectedTower= TowerConfig.TowerTypes.NONE;

    private List<Transform> turretList;

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
        turretList = new List<Transform>();
    }

    public Tower GetTower(TowerConfig.TowerTypes type)
    {
        Tower towerItem = Array.Find(towers, item => item.towerType == type);
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

    public void SetSelectedTower(TowerConfig.TowerTypes type)
    {
        switch(type)
        {
            case TowerConfig.TowerTypes.BASIC:
                selectedTower = TowerConfig.TowerTypes.BASIC;
                break;

            case TowerConfig.TowerTypes.HEAVY:
                selectedTower = TowerConfig.TowerTypes.HEAVY;
                break;

            case TowerConfig.TowerTypes.FREEZE:
                selectedTower = TowerConfig.TowerTypes.FREEZE;
                break;

        }

    }

    public void AddTurretToList(GameObject towerObj)
    {
        turretList.Add(towerObj.transform);
    }

    public void DestroyAllTurretsOnScreen()
    {
        if (turretList.Count > 0)
        {
            for (int i = 0; i < turretList.Count; i++)
            {
                if (turretList[i] != null)
                {
                    Destroy(turretList[i].transform.gameObject);
                }

            }
        }

        turretList = new List<Transform> ();

    }
}
