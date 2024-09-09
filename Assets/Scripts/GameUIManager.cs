using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currencyText;

    [SerializeField]
    private Button basicTurretButton;

    [SerializeField]
    private Button heavyTurretButton;

    [SerializeField]
    private Button freezeTurretButton;

    private void Awake()
    {
        basicTurretButton.onClick.AddListener(OnBasicButtonSelected);
        heavyTurretButton.onClick.AddListener(OnHeavyButtonSelected);
        freezeTurretButton.onClick.AddListener(OnFreezeButtonSelected);
    }

    private void OnGUI()
    {
        currencyText.text=GameManager.Instance.currency.ToString();
    }


    private void OnBasicButtonSelected()
    {
        TowerManager.Instance.SetSelectedTower(TowerTypes.BASIC);
    }

    private void OnHeavyButtonSelected()
    {
        TowerManager.Instance.SetSelectedTower(TowerTypes.HEAVY);
    }

    private void OnFreezeButtonSelected()
    {
        TowerManager.Instance.SetSelectedTower(TowerTypes.FREEZE);
    }
}
