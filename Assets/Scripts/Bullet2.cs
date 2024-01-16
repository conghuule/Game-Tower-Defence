using UnityEngine;

public class Bullet2 : Bullet
{
    public float splashDamageMultiplier = 0.5f;
    void spread()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (!enemy.GetComponent<Enemy>().checkIsDie() && distanceToEnemy < 1.5f && enemy.transform != target)
            {
                float splashDamage = Mathf.RoundToInt(damage * splashDamageMultiplier);
                enemy.GetComponent<Enemy>().TakeDamage(splashDamage);
            }
        }
    }
    public override void HitTarget()
    {
        spread();
        base.HitTarget();
    }
}