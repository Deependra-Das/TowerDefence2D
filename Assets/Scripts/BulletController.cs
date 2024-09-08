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

    private void FixedUpdate()
    {
        if (target!=null)
        {
            Vector2 direction = (target.position - transform.position).normalized;

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

}
