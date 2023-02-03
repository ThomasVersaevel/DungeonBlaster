using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAxe : AWeapon
{
    [SerializeField] private GameObject fireballCosmetic;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        //auS = gameObject.GetComponent<AudioSource>();
        attackSpeed = 8f;
        projectileSpeed = 6;
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
        sr.flipX = false;
        gameObject.transform.Rotate(0, 0, -Time.deltaTime * 200 * (3-attackTimer));
    }

    // Instantiate projectiles in an arc
    public override void Shoot(Vector3 mPos)
    {
        for (int i = 2; i < itemLevel + 3; i++)
        {
            base.Shoot(Quaternion.AngleAxis(0, Vector3.back) * new Vector3(1,1,0));
            base.Shoot(Quaternion.AngleAxis(0, Vector3.back) * new Vector3(-1, 1, 0));
            base.Shoot(Quaternion.AngleAxis(0, Vector3.back) * new Vector3(1, -1, 0));
            base.Shoot(Quaternion.AngleAxis(0, Vector3.back) * new Vector3(-1, -1, 0));
        }
    }
}
