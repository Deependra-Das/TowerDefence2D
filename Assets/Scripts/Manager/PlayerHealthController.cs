using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.GetComponent<EnemyController>())
        {
            AudioManager.Instance.PlaySFX(AudioTypeList.playerHurt);
            GameManager.Instance.DecreaseHealth(10);
        }

    }

}
