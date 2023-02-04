using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPojectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Player>().TakeDamage();
            Destroy(gameObject);
        }
        else if (coll.gameObject.tag == "Tile")
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject);
        }
    }
}
