using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform firePoint;  // Transform representing where bullets will be spawned
    public GameObject bulletPrefab;  // Prefab of the bullet
    private float nextFireTime = 0f;  // Time until the tower can fire again
    public int level = 1;
    public virtual float range { get; protected set; } = 3f;
    public virtual float fireRate { get; protected set; } = 1f;
    public virtual float slow { get; protected set; } = 0f;

    public virtual int dame { get; protected set; } = 5;
    public virtual int priceUpgrade { get; protected set; } = 10;

    public virtual int priceSell { get; protected set; } = 3;

    // public TowerProperties towerProperties;
    private GameObject towerPropertiesCanvas;
    public GameObject towerPropertiesPrefab;

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

            if (!enemy.GetComponent<Enemy>().checkIsDie() && distanceToEnemy < range && distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }
        }

        return nearestEnemy;
    }

    protected virtual void FireAt(Transform target)
    {
        GameObject bullet = Instantiate(bulletPrefab, new Vector2(firePoint.position.x, firePoint.position.y + 1.5f), firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = dame;
        bullet.GetComponent<Bullet>().slow = slow;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }

    public virtual void Upgrade()
    {

    }

    protected void OnMouseDown()
    {
        GameObject[] towersProperties = GameObject.FindGameObjectsWithTag("TowerProperties");
        foreach (GameObject towerProperties in towersProperties)
        {
            Destroy(towerProperties);
        }
        if (towerPropertiesCanvas == null)
        {
            towerPropertiesCanvas = Instantiate(towerPropertiesPrefab, Vector3.zero, Quaternion.identity);
        }
        towerPropertiesCanvas.GetComponent<TowerProperties>().setTower(this);
    }
}