using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidSpear : AWeapon
{

    [SerializeField] private GameObject fireballCosmetic;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        attackSpeed = 5f;
        damage = 3f;
        attackTimer = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        // auto fire weapon fires when ready, also only rotate weapon when not shooting
        if (attackTimer < 0)
        {
            attackTimer = attackSpeed;
            Shoot(mousePos);
            AttackAnimation();
        }
        else if (attackTimer < 3f) // swipe  the sword to show attack
        {
            AttackAnimation();
        }
        else
        {
            sr.enabled = false;
            UpdateRotation();
        }
        sr.enabled = attackTimer < 3f;
        attackTimer -= Time.deltaTime;
    }

    // rotate axe slowly and spawn the fireballs over 3 seconds
    private void AttackAnimation()
    {
       // gameObject.transform.position;
    }

    // Instantiate black hole projectiles at positions relative to the player then have them expand and dmg
    public override void Shoot(Vector3 mPos)
    {
         // instantiate black holes at positions relative to player, they dont move they pulse once and disapear
         Instantiate(projectile, new Vector3(0, 5, 0), transform.rotation);
         Instantiate(projectile, new Vector3(5, -4, 0), transform.rotation);
         Instantiate(projectile, new Vector3(-5, -4, 0), transform.rotation);
    }
}
