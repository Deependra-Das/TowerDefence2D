using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseController : MonoBehaviour
{

    [SerializeField]
    private LobbyController lobbyControllerObj;

    [SerializeField]
    private Button ResumeGameButton;

    [SerializeField]
    private Button BackButton;

    private void Start()
    {
        ResumeGameButton.onClick.AddListener(OnResumeGameButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnResumeGameButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioConfig.AudioNames.buttonMenuClick);
        UIManager.Instance.SetHoveringState(false);
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    private void OnBackButtonClick()
    {
        AudioManager.Instance.PlaySFX(AudioConfig.AudioNames.buttonMenuClick);
        this.gameObject.SetActive(false);

        lobbyControllerObj.gameObject.SetActive(true);

    }
}
