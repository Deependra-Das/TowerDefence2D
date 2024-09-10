using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnManager enemySpawnManagerObj;

    [SerializeField]
    private LobbyController lobbyControllerObj;

    [SerializeField]
    private Button RestartGameButton;

    [SerializeField]
    private Button BackButton;

    private void Start()
    {
        RestartGameButton.onClick.AddListener(OnRestartGameButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnRestartGameButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        AudioManager.Instance.MuteAudioSource(AudioSourceList.audioSourceBGM, false);
        AudioManager.Instance.PlayBGM(AudioTypeList.backgroundMusic);
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        GameManager.Instance.ResetGameManager();
        enemySpawnManagerObj.DestroyAllEnemiesOnScreen();
        TowerManager.Instance.DestroyAllTurretsOnScreen();
        enemySpawnManagerObj.StartSpawner();
    }

    private void OnBackButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        this.gameObject.SetActive(false);

        lobbyControllerObj.gameObject.SetActive(true);

    }

}
