using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow: AWeapon
{ 
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        //auS = gameObject.GetComponent<AudioSource>();
        attackSpeed = 4.5f;
        projectileSpeed = 7;
        damage = 3f;
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
        else if (attackTimer < 0.15f) // swipe  the sword to show attack
        {
            AttackAnimation();
        }
        else
        {
            sr.enabled = false;
            UpdateRotation();
        }
        sr.enabled = attackTimer < 1.55f;
        attackTimer -= Time.deltaTime;
    }

    private void AttackAnimation()
    {
        //gameObject.transform.position -= mousePos * Time.deltaTime;
    }

    // Pull back crossbow and shoot piercing arrow
    public override void Shoot(Vector3 mPos)
    {
        base.Shoot(mPos);

    }
}
