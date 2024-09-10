using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private float bulletSpeed=5f;

    [SerializeField]
    private Rigidbody2D rb2D_Bullet;

    [SerializeField]
    private int bulletPower = 1;

    private Vector2 direction;

    private void Start()
    {
        RotateTowardsTarget();
    }

    private void FixedUpdate()
    {
        if (target!=null)
        {
            direction = (target.position - transform.position).normalized;
            rb2D_Bullet.velocity = direction * bulletSpeed;
        }
    }

    public void SetTarget(Transform targetEnemy)
    {
        target=targetEnemy;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<EnemyController>().TakeDamge(bulletPower);
        Destroy(gameObject);
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = targetRotation;
    }

}
