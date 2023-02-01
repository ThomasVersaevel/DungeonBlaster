using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAxeProjectile : AProjectile
{
    private float rotation;
    private float pulseDelay = 0.8f;
    private float pulseTimer;
    private CircleCollider2D coll;

    private void Start()
    {
        coll = gameObject.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        // only enable collider every so often to pulse dmg
        coll.enabled = pulseTimer < 0;
        pulseTimer -= Time.deltaTime;

        //if (coll.enabled) print("colliding");

        rotation += 1000 * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    // on enemy hit disable hitbox and 
    protected override void EnemyHit(GameObject enemy)
    {
        enemy.GetComponent<AbstractEnemy>().TakeDamage(damage);
        coll.enabled = false;
        pulseTimer = pulseDelay;
    }
}
