using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<AbstractEnemy>().TakeDamage(damage); 
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject);


        } else if (coll.gameObject.tag == "Tile")
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject); 
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<AbstractEnemy>().TakeDamage(damage); 
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject);
        }
        else if (coll.gameObject.tag == "Tile")
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject);
        }
    }
}
