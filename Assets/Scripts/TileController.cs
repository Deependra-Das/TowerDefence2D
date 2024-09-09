using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr_Tile;

    private GameObject tower;

    private Color initialColor;

    [SerializeField]
    private Color colorOnHover;
    void Start()
    {
        initialColor=sr_Tile.color; 
    }

    private void OnMouseEnter()
    {
        sr_Tile.color=colorOnHover;
    }

    private void OnMouseExit()
    {
        sr_Tile.color = initialColor;
    }

    private void OnMouseDown()
    {
        if (tower == null)
        {
            BuildTower();
        }
    }

    private void BuildTower()
    {
        Tower newTowerBuild = TowerManager.Instance.GetSelectedTower();
        if(newTowerBuild==null)
        {
            Debug.Log("Select a Tower to Build.");
            return;
        }
        if(newTowerBuild.buildCost > GameManager.Instance.currency)
        {
            Debug.Log("Insufficient Funds. Earn More Currency.");
            return;
        }
        GameManager.Instance.SpendCurrency(newTowerBuild.buildCost);    

        Instantiate(newTowerBuild.towerPrefab, transform.position, Quaternion.identity);
    }
}
