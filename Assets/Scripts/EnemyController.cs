using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Rigidbody2D rb2D_Enemy;

    [SerializeField]
    private int enemyHealth = 2;

    [SerializeField]
    private int currencyDrop = 50;

    private Transform destination;
    private int pathIndex = 0;

    private bool isDead;

    private float currentSpeed;
    
    void Start()
    {
        destination = GameManager.Instance.path[pathIndex];
        isDead = false;
        currentSpeed = moveSpeed;
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
        rb2D_Enemy.velocity= direction * currentSpeed;

    }

    public void TakeDamge(int damageValue)
    {
        enemyHealth-=damageValue;

        if(enemyHealth<=0 && !isDead)
        {
            EnemySpawnManager.onEnemyDestroy.Invoke();
            AudioManager.Instance.PlayEnemySFX(AudioTypeList.enemyDeath);
            GameManager.Instance.AddCurrency(currencyDrop);
            isDead = true;
            Destroy(gameObject);
        }
    }

    public void UpdateSpeed(float speedValue)
    {
        currentSpeed = speedValue;
    }

    public void ResetSpeed()
    {
        currentSpeed = moveSpeed;
    }
}
