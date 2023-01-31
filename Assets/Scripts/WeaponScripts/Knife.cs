using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : AWeapon
{
    private float rotation;
    private bool attackAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        attackSpeed = 0.5f;
        damage = 1f;
        projectileSpeed = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && attackTimer < 0)
        {
            attackTimer = attackSpeed;
            Shoot(mousePos);
        }
        else
        {
            attackTimer -= Time.deltaTime;
            // adjust UIitem cooldown fill here
        }
        UpdateRotation(90);
        //temporary solution
        itemLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getLevel();
    }

    public override void Shoot(Vector3 mPos)
    {
        for (int i = 0; i < itemLevel; i++) { // one knife per lvl
            int angleOffset = 8 + itemLevel * 2;
            base.Shoot(Quaternion.AngleAxis(Random.Range(-angleOffset, angleOffset), Vector3.back) * mPos);
        }
    }
}
