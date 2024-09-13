using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr_Tile;

    private GameObject towerObj;

    [SerializeField]
    private TurretController turretControllerObj;

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
        if(UIManager.Instance.CheckHoveringState())
        {
            return;
        }

        if (towerObj == null)
        {
            BuildTower();
        }
        else
        {
            turretControllerObj.OpenUIPanel();
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
        if(newTowerBuild.buildCost > CurrencyManager.Instance.currency)
        {
            Debug.Log("Insufficient Funds. Earn More Currency.");
            return;
        }
        CurrencyManager.Instance.SpendCurrency(newTowerBuild.buildCost);
        AudioManager.Instance.PlaySFX(AudioConfig.AudioNames.turretPlaced);
        towerObj =Instantiate(newTowerBuild.towerPrefab, transform.position, Quaternion.identity);
        TowerManager.Instance.AddTurretToList(towerObj);
        turretControllerObj = towerObj.GetComponent<TurretController>();

    }

}
