using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : AWeapon
{
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
    }
}
