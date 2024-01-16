using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy3 : Enemy
{
    public Enemy3()
    {
        maxHealth = 100f;
    }
    public override void TakeDamage(float dame)
    {
        base.TakeDamage(dame);
    }
}
