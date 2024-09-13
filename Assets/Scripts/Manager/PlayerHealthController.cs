using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.GetComponent<EnemyController>())
        {
            AudioManager.Instance.PlaySFX(AudioConfig.AudioNames.playerHurt);
            GameManager.Instance.DecreaseHealth(10);
        }

    }

}
