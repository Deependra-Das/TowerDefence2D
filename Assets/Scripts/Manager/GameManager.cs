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

    [SerializeField]
    private PlayerHealthController playerHealthControllerObj;

    public Transform startPoint;
    public Transform[] path;

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
        isGameOver = false;
        playerHealthControllerObj.ResetPlayerHealth();
        CurrencyManager.Instance.ResetCurrency();
        gameOverControllerObj.gameObject.SetActive(false);
        gameCompletedControllerObj.gameObject.SetActive(false);
        gamePauseControllerObj.gameObject.SetActive(false);
        UIManager.Instance.SetHoveringState(false);
    }

    public void setGameOver()
    {
        isGameOver = true;
        OnGameOver();
    }

    public void OnGameOver()
    {
        AudioManager.Instance.MuteAudioSource(AudioConfig.AudioSourceList.audioSourceBGM, true);
        AudioManager.Instance.PlaySFX(AudioConfig.AudioNames.gameOver);
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
