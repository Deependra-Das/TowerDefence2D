using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LobbyController : MonoBehaviour
{

    [SerializeField]
    private EnemySpawnManager enemySpawnManagerObj;

    [SerializeField]
    private TileController tileControllerObj;

    [SerializeField]
    private Button StartGameButton;

    [SerializeField]
    private Button QuitGameButton;

    private void Start()
    {
        StartGameButton.onClick.AddListener(OnStartGameButtonClick);
        QuitGameButton.onClick.AddListener(OnQuitGameButtonClick);
        AudioManager.Instance.MuteAudioSource(AudioSourceList.audioSourceBGM, false);
        AudioManager.Instance.PlayBGM(AudioTypeList.backgroundMusic);
    }

    private void OnStartGameButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        this.gameObject.SetActive(false);
        GameManager.Instance.ResetGameManager();
        enemySpawnManagerObj.DestroyAllEnemiesOnScreen();
        TowerManager.Instance.DestroyAllTurretsOnScreen();
        enemySpawnManagerObj.StartSpawner();
    }

    private void OnQuitGameButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        Application.Quit();
    }

}
