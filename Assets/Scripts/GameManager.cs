using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField]
    private GameUIManager gameUIManagerObj;

    [SerializeField]
    private GameOverController gameOverControllerObj;

    [SerializeField]
    private GameCompletedController gameCompletedControllerObj;

    [SerializeField]
    private GamePauseController gamePauseControllerObj;

    public Transform startPoint;
    public Transform[] path;

    public int currency;

    [SerializeField]
    private int playerHealth;

    [SerializeField]
    private int MaxHealth;

    [SerializeField]
    private int initialCurrency;

    public bool isGameOver;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameOver==false)
        {
            OnGamePaused();
            Time.timeScale = 0f;
        }
    }

    public void ResetGameManager()
    {
        currency = initialCurrency;
        playerHealth = MaxHealth;
        gameUIManagerObj.setMaxHealth(MaxHealth);
        isGameOver = false;
        gameOverControllerObj.gameObject.SetActive(false);
        gameCompletedControllerObj.gameObject.SetActive(false);
        gamePauseControllerObj.gameObject.SetActive(false);
        UIManager.Instance.SetHoveringState(false);
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount; 
            return true;
        }
        else
        {
            Debug.Log("Insufficient Funds");
            return false;
        }
    }

    public void DecreaseHealth(int amount)
    {
        playerHealth-=amount;
        gameUIManagerObj.setHealth(playerHealth);

        if (playerHealth<=0)
        {
            playerHealth = 0;
            isGameOver = true;
            OnGameOver();
        }
    }

    public void OnGameOver()
    {
        AudioManager.Instance.MuteAudioSource(AudioSourceList.audioSourceBGM, true);
        AudioManager.Instance.PlaySFX(AudioTypeList.gameOver);
        Time.timeScale = 0f;
        UIManager.Instance.SetHoveringState(true);
        gameOverControllerObj.gameObject.SetActive(true);
    }

    public void OnGameCompleted()
    {
        Time.timeScale = 0f;
        UIManager.Instance.SetHoveringState(true);
        gameCompletedControllerObj.gameObject.SetActive(true);
    }

    public void OnGamePaused()
    {
        Time.timeScale = 0f;
        UIManager.Instance.SetHoveringState(true);
        gamePauseControllerObj.gameObject.SetActive(true);
    }
}
