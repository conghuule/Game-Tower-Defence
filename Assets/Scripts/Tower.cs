using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform firePoint;  // Transform representing where bullets will be spawned
    public GameObject bulletPrefab;  // Prefab of the bullet
    public float fireRate = 1f;  // Rate of fire in shots per second
    private float nextFireTime = 0f;  // Time until the tower can fire again
    private float range = 2f;  // Time until the tower can fire again

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            GameObject nearestEnemy = FindNearestEnemy();

            // If an enemy is found, fire at it
            if (nearestEnemy != null)
            {
                FireAt(nearestEnemy.transform);
                nextFireTime = Time.time + 1f / fireRate;  // Update the next fire time
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        // Find all GameObjects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Iterate through all enemies and find the nearest one
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            Debug.Log(distanceToEnemy);
            if (!enemy.GetComponent<Enemy>().checkIsDie() && distanceToEnemy < range && distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    void FireAt(Transform target)
    {
        // Instantiate a bullet at the firePoint position and rotation
        GameObject bullet = Instantiate(bulletPrefab, new Vector2(firePoint.position.x, firePoint.position.y + 1.5f), firePoint.rotation);

        // Set the target for the bullet to follow
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }
}