using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.maxValue = 100;
        slider.value = 100;
    }

    public void setHealth(int health)
    {
        slider.value = health;
    }

}
