using UnityEngine;

public class Bullet1 : Bullet
{
    public override void HitTarget()
    {
        base.slowEnemy();
        base.HitTarget();
    }
}