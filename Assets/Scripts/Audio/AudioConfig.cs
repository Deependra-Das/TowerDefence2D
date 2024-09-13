using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioConfig : MonoBehaviour
{
    public enum AudioNames
    {
        backgroundMusic,
        buttonMenuClick,
        bulletShot,
        missileShot,
        freezeShot,
        turretPlaced,
        turretUpgrade,
        turretRemove,
        enemyDeath,
        playerHurt,
        gameOver
    }

    public enum AudioSourceList
    {
        audioSourceSFX,
        audioSourceBGM,
        audioSourceEnemy,
        audioSourceTurret,

    }
}
