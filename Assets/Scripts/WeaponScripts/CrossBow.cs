using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow: AWeapon 
{
    
   // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        auS = gameObject.GetComponent<AudioSource>();
        attackSpeed = 0.5f;
        projectileSpeed = 8;
        damage = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        level = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getLevel();
    }
    public override void Shoot(Vector3 mousePos)
    {
        var startPos = Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(mousePos.x - startPos.x, mousePos.y - startPos.y) * Mathf.Rad2Deg;

        for (int i = 0; i < level; i++)
        { // one knife per lvl
            int angleOffset = 8 + level * 2;

            GameObject proj = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
            proj.GetComponent<Rigidbody2D>().velocity = mousePos.normalized * projectileSpeed;
            proj.GetComponent<AProjectile>().damage = damage;
            //base.Shoot(Quaternion.AngleAxis(Random.Range(-angleOffset, angleOffset), Vector3.back) * mousePos);
        }
    }
}
