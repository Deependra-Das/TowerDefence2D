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
        GameObject newTowerBuild = TowerManager.Instance.GetSelectedTower();
        Instantiate(newTowerBuild, transform.position, Quaternion.identity);
    }
}
