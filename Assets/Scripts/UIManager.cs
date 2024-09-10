using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    private bool isHoveringOnUI;

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

    public void SetHoveringState(bool state)
    {
        isHoveringOnUI = state;
    }

    public bool CheckHoveringState()
    {
        return isHoveringOnUI;
    }
}
