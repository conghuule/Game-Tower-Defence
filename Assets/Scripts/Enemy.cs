using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem smokeEffect;
    [SerializeField] private AudioClip fixClip;

    [SerializeField] private int currencyWorth = 50;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    public virtual float maxHealth { get; protected set; } = 20f;
    protected float currentHealth;
    protected bool isDie = false;

    protected Transform target;
    protected int pathIndex = 0;
    Rigidbody2D rb;

    protected AudioSource audioSource;
    protected FloatHealthBar healthBar;
    protected Animator animator;

    public Enemy()
    {
        maxHealth = 20f;
    }

    void Start()
    {
        currentHealth = maxHealth;
        target = LevelManager.main.path[pathIndex];
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        healthBar = GetComponentInChildren<FloatHealthBar>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        // Use MoveTowards for controlled movement
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target.position, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        // Check if the enemy has reached the target point
        if (Vector2.Distance(transform.position, target.position) < 0.5f)
        {
            UpdateTarget();

            // Check if the current target is Point 4
            if (pathIndex == 4) // assuming index 3 corresponds to Point 4 (arrays start from index 0)
            {
                // Flip the enemy's direction when reaching Point 4
                FlipDirection();
            }
        }
    }

    void FlipDirection()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    void UpdateTarget()
    {
        pathIndex++;
        if (pathIndex == LevelManager.main.path.Length)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject); ;
        }
        else
        {
            target = LevelManager.main.path[pathIndex];
        }
    }

    void DestroyEnemy()
    {
        Die();
    }

    public void Die()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        animator.SetTrigger("Fixed");
        Destroy(smokeEffect.gameObject);

        PlaySound(fixClip);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public virtual void TakeDamage(float dame)
    {
        currentHealth -= dame;
        healthBar.updateHealthBar(currentHealth, maxHealth);
        bool isEnemyDie = currentHealth <= 0;
        if (isEnemyDie)
        {
            isDie = isEnemyDie;
            LevelManager.main.IncreaseCurrency(currencyWorth);
            animator.SetBool("Die", true);
            GetComponent<Rigidbody2D>().simulated = false;
            Destroy(gameObject, 1.2f);


            LevelManager.main.IncreaseScore();
        }
    }

    public bool checkIsDie()
    {
        return isDie;
    }
}
