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
        UpdateMeleeRotation(0);
        level = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getLevel();

    }

    public override void Shoot(Vector3 mousePos)
    {
        rotation += 500 * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, rotation);
        for (int i = 0; i < level; i++) { // one knife per lvl
            int angleOffset = 8 + level * 2;
            base.Shoot(Quaternion.AngleAxis(Random.Range(-angleOffset, angleOffset), Vector3.back) * mousePos);
        }
    }
}
