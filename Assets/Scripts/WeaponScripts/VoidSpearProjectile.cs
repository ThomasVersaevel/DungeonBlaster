using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidSpearProjectile : AProjectile
{
    private float size = 0;
    private float pulseDelay = 0.8f;
    private float pulseTimer = 0;
    private CircleCollider2D coll;

    private void Start()
    {
        coll = gameObject.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        // only enable collider every so often to pulse dmg
        // coll.enabled = pulseTimer < 0;
        pulseTimer -= Time.deltaTime;

        //if (coll.enabled) print("colliding");

        size += 0.01f * Time.deltaTime;
        gameObject.transform.localScale = new Vector3(size, size, 1);
    }

    // on enemy hit disable hitbox and 
    protected override void EnemyHit(GameObject enemy)
    {
        enemy.GetComponent<AbstractEnemy>().TakeDamage(damage);
        coll.enabled = false;
        pulseTimer = pulseDelay;
    }
}
