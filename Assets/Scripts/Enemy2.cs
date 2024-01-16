using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy2 : Enemy
{
    public Enemy2()
    {
        maxHealth = 50f;
    }
    public override void TakeDamage(float dame)
    {
        base.TakeDamage(dame);
    }
}
