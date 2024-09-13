using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField]
    private GameUIManager gameUIManagerObj;

    [SerializeField]
    private int playerHealth;

    [SerializeField]
    private int MaxHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.GetComponent<EnemyController>())
        {
            AudioManager.Instance.PlaySFX(AudioConfig.AudioNames.playerHurt);
           DecreaseHealth(10);
        }

    }

    private void DecreaseHealth(int amount)
    {
        playerHealth -= amount;
        gameUIManagerObj.setHealth(playerHealth);

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            GameManager.Instance.setGameOver();
        }
    }

    public void ResetPlayerHealth()
    {
        playerHealth = MaxHealth;
        gameUIManagerObj.setMaxHealth(MaxHealth);
    }


}
