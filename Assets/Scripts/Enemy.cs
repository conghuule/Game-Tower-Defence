using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float maxHealth = 20f;
    private float currentHealth;
    public bool vertical;
    Rigidbody2D rigidbody2d;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    Animator animator;

    private bool isDie = false;
    public ParticleSystem smokeEffect;

    AudioSource audioSource;
    public AudioClip fixClip;

    FloatHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<FloatHealthBar>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction; timer = changeTime;
        }
    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        animator.SetBool("isRunRight", direction > 0);
        rigidbody2d.MovePosition(position);
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

    public void TakeDamage(float dame)
    {
        currentHealth -= dame;
        healthBar.updateHealthBar(currentHealth, maxHealth);
        bool isEnemyDie = currentHealth <= 0;
        if (isEnemyDie)
        {
            isDie = isEnemyDie;
            animator.SetBool("Die", true);
            GetComponent<Rigidbody2D>().simulated = false;
            Destroy(gameObject, 1.2f);
        }
    }

    public bool checkIsDie()
    {
        return isDie;
    }
}
