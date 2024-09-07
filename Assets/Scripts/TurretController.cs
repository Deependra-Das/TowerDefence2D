using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private float targetingRange = 3f;

    [SerializeField]
    private Transform turretRotationPoint;

    [SerializeField]
    private float rotationSpeed=5f;

    [SerializeField]
    private LayerMask enemyMask;

    private Transform target;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else if (target != null)
        {
            if (!checkTargetInRange())
            {
                target = null;
            }
            else
            {

                RotateTowardsTarget();
            }
        }
  
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target=hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg -90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool checkTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
}
