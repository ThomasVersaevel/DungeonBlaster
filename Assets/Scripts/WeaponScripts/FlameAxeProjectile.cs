using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAxeProjectile : AProjectile
{
    private float rotation;
    private float pulseDelay = 0.2f;
    private float pulseTimer;
    private CircleCollider2D coll;

    void Update()
    {
        // only enable collider every so often to pulse dmg
        coll.enabled = pulseTimer < 0;
        if (pulseTimer < 0)
        {
            pulseTimer = pulseDelay;
        }
        pulseTimer -= Time.deltaTime;
        rotation += 1000 * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    protected override void EnemyHit(GameObject enemy)
    {
        enemy.GetComponent<AbstractEnemy>().TakeDamage(damage);
    }
}
