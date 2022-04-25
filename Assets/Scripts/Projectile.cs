using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float damage;
    public float nig;

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
            coll.gameObject.SendMessage("TakeDamage", damage);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject, 1.5f);


        } else
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject, 3); 
        }
    }
}
