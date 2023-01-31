using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSword : AWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        //auS = gameObject.GetComponent<AudioSource>();
        attackSpeed = 2.5f;
        projectileSpeed = 6;
        damage = 1f;
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
        } else
        {
            sr.enabled = false;
            UpdateRotation();
        }
        sr.enabled = attackTimer < 0.35f;
        attackTimer -= Time.deltaTime;
    }

    // whip sword
    private void AttackAnimation()
    {
        gameObject.transform.Rotate(0, 0, -Time.deltaTime * 1100);
    }

    // Instantiate projectiles in an arc
    public override void Shoot(Vector3 mPos)
    {
        for (int i = 2; i < itemLevel+3; i++)
        {
            base.Shoot(Quaternion.AngleAxis(0, Vector3.back) * mPos);
            base.Shoot(Quaternion.AngleAxis(30, Vector3.back) * mPos);
            base.Shoot(Quaternion.AngleAxis(-30, Vector3.back) * mPos);
        }
       
    }
}
