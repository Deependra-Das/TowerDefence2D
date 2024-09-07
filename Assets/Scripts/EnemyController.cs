using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Rigidbody2D rb2D_Enemy;

    private Transform destination;
    private int pathIndex = 0;
    
    void Start()
    {
        destination = GameManager.Instance.path[pathIndex];
    }

    void Update()
    {
        if(Vector2.Distance(destination.position, transform.position)<=0.1f)
        {
            pathIndex++;
           
            if (pathIndex >= GameManager.Instance.path.Length)
            {
                EnemySpawnManager.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                destination = GameManager.Instance.path[pathIndex];

            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (destination.position - transform.position).normalized;
        rb2D_Enemy.velocity= direction * moveSpeed;

    }
}
