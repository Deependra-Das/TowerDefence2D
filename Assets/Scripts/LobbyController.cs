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
    }

    private void OnStartGameButtonClick()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.ResetGameManager();
        enemySpawnManagerObj.DestroyAllEnemiesOnScreen();
        TowerManager.Instance.DestroyAllTurretsOnScreen();
        enemySpawnManagerObj.StartSpawner();
    }

    private void OnQuitGameButtonClick()
    {
        Application.Quit();
    }

}
