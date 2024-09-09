using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurretController : MonoBehaviour
{

    [SerializeField]
    private float targetingRadius = 3f;

    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private LayerMask enemyMask;

    [SerializeField]
    private float rateOfFire = 1f;

    [SerializeField]
    private Transform turretRotationPoint;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform firingPoint;

    private Transform target;

    private float timeUntilFire = 0f;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();

        if (!CheckTargetInRadius())
        {
            target = null;
        }
        else
        {

            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / rateOfFire)
            {
                ShootBullet();
                timeUntilFire = 0;
            }
        }


    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRadius, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetInRadius()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRadius;
    }

    private void ShootBullet()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        BulletController bulletController = bulletObj.GetComponent<BulletController>();

        bulletController.SetTarget(target);
    }

}
