using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AProjectile : MonoBehaviour
{

    public float damage;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            EnemyHit(coll.gameObject);
        }
        else if (coll.gameObject.tag == "Tile")
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject); 
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            EnemyHit(coll.gameObject);
        }
        else if (coll.gameObject.tag == "Tile") // TODO decide on wall collision
        {
            //Destroy(gameObject.GetComponent<BoxCollider2D>());
            //Destroy(gameObject);
        }
    }
    // method to be overriden by exploding projectiles and such
    protected virtual void EnemyHit(GameObject enemy)
    {
        enemy.GetComponent<AbstractEnemy>().TakeDamage(damage);
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject);
    }
}
