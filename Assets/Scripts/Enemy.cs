using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem smokeEffect;
    [SerializeField] private AudioClip fixClip;

    [SerializeField] private int currencyWorth = 1;

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

    protected float speedMultiplier = 1f;
    protected float originalSpeedMultiplier = 1f;

    public Enemy()
    {
        maxHealth = 20f;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth; // Reset currentHealth when maxHealth changes
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
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target.position, moveSpeed * speedMultiplier * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        // Check if the enemy has reached the target point
        if (Vector2.Distance(transform.position, target.position) < 0.5f)
        {
            UpdateTarget();

            // Check if the current target is Point 4
            if (target.position == LevelManager.main.flipPoint.position) // assuming index 3 corresponds to Point 4 (arrays start from index 0)
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
            LevelManager.main.MinusHealth();
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
        else
        {
            target = LevelManager.main.path[pathIndex];
        }
    }

    public virtual void TakeSlow(float slowDuration, float slowAmount)
    {
        originalSpeedMultiplier = speedMultiplier;

        speedMultiplier = 1 - slowAmount;

        StartCoroutine(RevertSlowEffect(slowDuration));
    }

    IEnumerator RevertSlowEffect(float duration)
    {
        yield return new WaitForSeconds(duration);

        speedMultiplier = originalSpeedMultiplier;
    }

    public virtual void TakeDamage(float dame)
    {
        currentHealth -= dame;
        if (healthBar != null)
            healthBar.updateHealthBar(currentHealth, maxHealth);
        bool isEnemyDie = currentHealth <= 0;
        if (isEnemyDie)
        {
            LevelManager.main.PlayDieClip();
            isDie = isEnemyDie;
            LevelManager.main.IncreaseCurrency(currencyWorth);
            animator.SetBool("Die", true);
            GetComponent<Rigidbody2D>().simulated = false;
            Destroy(gameObject, 1.2f);
            EnemySpawner.onEnemyDestroy.Invoke();


            LevelManager.main.IncreaseScore();
        }
    }

    public bool checkIsDie()
    {
        return isDie;
    }
}
