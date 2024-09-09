using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTurretController : MonoBehaviour
{

    [SerializeField]
    private float targetingRadius = 2f;

    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private LayerMask enemyMask;

    [SerializeField]
    private float rateOfFire = 4f;

    [SerializeField]
    private Transform turretRotationPoint;

    [SerializeField]
    private float freezemoveSpeed=0.5f;

    [SerializeField]
    private float freezeDuration = 1f;

    private float timeUntilFire = 0f;


    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / rateOfFire)
        {
            FreezeEnemiesinRadius();
            timeUntilFire = 0;
        }


    }

    private void FreezeEnemiesinRadius()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRadius, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hitObject= hits[i];

                EnemyController enemyObject=hitObject.transform.GetComponent<EnemyController>();
                enemyObject.UpdateSpeed(freezemoveSpeed);

                StartCoroutine(ResetEnemySpeed(enemyObject));
            }
        }

    }

    private IEnumerator ResetEnemySpeed(EnemyController enemyObject)
    {
        yield return new WaitForSeconds(freezeDuration);
        enemyObject.ResetSpeed();

    }


}
