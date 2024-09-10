using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LobbyController : MonoBehaviour
{
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
        SceneManager.LoadScene(1);
    }

    private void OnQuitGameButtonClick()
    {
        Application.Quit();
    }

}
