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

    [SerializeField]
    private Animator enemyAnimator;

    void Start()
    {
        destination = GameManager.Instance.path[pathIndex];
        isDead = false;
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        FindPath();
    }

    private void FixedUpdate()
    {
        Move();
        MovementAnimation();
    }

    private void FindPath()
    {
        if (Vector2.Distance(destination.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex >= GameManager.Instance.path.Length)
            {
                EnemySpawnManager.Instance.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                destination = GameManager.Instance.path[pathIndex];

            }
        }
    }

    private void Move()
    {
        Vector2 direction = (destination.position - transform.position).normalized;
        rb2D_Enemy.velocity = direction * currentSpeed;
    }

    private void MovementAnimation()
    {
        if ((int)rb2D_Enemy.velocity.x > 0 && (int)rb2D_Enemy.velocity.y == 0)
        {
            SetDirectionForAnimation(true,false,false, false);
        }

        else if ((int)rb2D_Enemy.velocity.x < 0 && (int)rb2D_Enemy.velocity.y == 0)
        {
            SetDirectionForAnimation(false, true, false, false);
        }

        else if ((int)rb2D_Enemy.velocity.x == 0 && (int)rb2D_Enemy.velocity.y > 0)
        {
            SetDirectionForAnimation(false, false, true, false);
        }

        else if ((int)rb2D_Enemy.velocity.x == 0 && (int)rb2D_Enemy.velocity.y < 0)
        {
            SetDirectionForAnimation(false, false, false, true);
        }
    }

    private void SetDirectionForAnimation(bool right, bool left, bool top, bool bottom)
    {
        enemyAnimator.SetBool("Right", right);
        enemyAnimator.SetBool("Left", left);
        enemyAnimator.SetBool("Top", top);
        enemyAnimator.SetBool("Bottom", bottom);
    }

    public void TakeDamge(int damageValue)
    {
        enemyHealth-=damageValue;

        if(enemyHealth<=0 && !isDead)
        {
            EnemySpawnManager.Instance.onEnemyDestroy.Invoke();
            AudioManager.Instance.PlayEnemySFX(AudioConfig.AudioNames.enemyDeath);
            CurrencyManager.Instance.AddCurrency(currencyDrop);
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
