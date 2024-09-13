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

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private TextMeshProUGUI waveText;

    [SerializeField]
    private TextMeshProUGUI labelText;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private Image[] buttonContainerList;

    private float remainingTime;

    private void Awake()
    {
        basicTurretButton.onClick.AddListener(OnBasicButtonSelected);
        heavyTurretButton.onClick.AddListener(OnHeavyButtonSelected);
        freezeTurretButton.onClick.AddListener(OnFreezeButtonSelected);
    }
    private void Update()
    {
        if (timerText.enabled)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
            }
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
    public void setMaxHealth(int healthValue)
    {
        slider.maxValue = healthValue;
        slider.value = healthValue;
    }

    public void setHealth(int healthValue)
    {
        slider.value = healthValue;
    }

    private void OnGUI()
    {
        currencyText.text=CurrencyManager.Instance.currency.ToString();
    }


    private void OnBasicButtonSelected()
    {
        AudioManager.Instance.PlaySFX(AudioConfig.AudioNames.buttonMenuClick);
        TowerManager.Instance.SetSelectedTower(TowerConfig.TowerTypes.BASIC);
        SetTurretToPlace(TowerConfig.TowerTypes.BASIC);
    }

    private void OnHeavyButtonSelected()
    {
        AudioManager.Instance.PlaySFX(AudioConfig.AudioNames.buttonMenuClick);
        TowerManager.Instance.SetSelectedTower(TowerConfig.TowerTypes.HEAVY);
        SetTurretToPlace(TowerConfig.TowerTypes.HEAVY);
    }

    private void OnFreezeButtonSelected()
    {
        AudioManager.Instance.PlaySFX(AudioConfig.AudioNames.buttonMenuClick);
        TowerManager.Instance.SetSelectedTower(TowerConfig.TowerTypes.FREEZE);
        SetTurretToPlace(TowerConfig.TowerTypes.FREEZE);
    }

    public void SetWaveText(string textValue)
    {
        waveText.text = textValue;
    }
    public void SetLabelText(string textValue)
    {
        labelText.text = textValue;
    }
    public void SetTimerText(string textValue)
    {
        timerText.text = textValue;
    }
    public void ShowTimerText(float countdownValue)
    {
        remainingTime = countdownValue;
        timerText.enabled = true;
    }
    public void HideTimerText()
    {
        timerText.enabled = false;
    }

    public void SetTurretToPlace(TowerConfig.TowerTypes type)
    {
        setAllButtonToDefaultState();

        switch(type)
        {
            case TowerConfig.TowerTypes.BASIC:
                buttonContainerList[0].color = Color.black;
                break;
            case TowerConfig.TowerTypes.HEAVY:
                buttonContainerList[1].color = Color.black;
                break;
            case TowerConfig.TowerTypes.FREEZE:
                buttonContainerList[2].color = Color.black;
                break;
        }
    }

    private void setAllButtonToDefaultState()
    {
        foreach (Image turbutcon in buttonContainerList)
        {
            turbutcon.color = Color.white;
        }
    }
}
