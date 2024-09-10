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
    private Button RetartGameButton;

    [SerializeField]
    private Button BackButton;

    private void Start()
    {
        RetartGameButton.onClick.AddListener(OnStartGameButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
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

    private void OnBackButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioTypeList.buttonMenuClick);
        this.gameObject.SetActive(false);

        lobbyControllerObj.gameObject.SetActive(true);

    }

}
