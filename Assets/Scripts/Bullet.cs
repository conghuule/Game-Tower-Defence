using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;  // Speed of the bullet
    public int damage = 10;  // Damage dealt to enemies
    public Transform target;  // Target enemy to follow
    public GameObject explosionPrefab;
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            // If the target is null, destroy the bullet
            Destroy(gameObject);
            return;
        }

        // Move towards the target
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // If the bullet is close enough to the target, deal damage and destroy itself
        if (direction.magnitude <= distanceThisFrame * 2)
        {
            HitTarget();
            return;
        }

        // Move the bullet towards the target
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // Deal damage to the enemy
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Instantiate the explosion prefab at the bullet's position
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Destroy the explosion prefab after a delay
        Destroy(explosion, 0.5f);
        // Destroy the bullet
        Destroy(gameObject);
    }
}