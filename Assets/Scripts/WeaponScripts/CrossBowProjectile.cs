using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowProjectile : AProjectile
{
    public int lifeTime = 2;
    protected override void EnemyHit(GameObject enemy)
    {
        lifeTime--;
        enemy.GetComponent<AbstractEnemy>().TakeDamage(damage);
        if (lifeTime <= 0)
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject);
        }
    }
}
