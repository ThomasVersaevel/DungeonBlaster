using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : AWeapon
{

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        auS = gameObject.GetComponent<AudioSource>();
        attackSpeed = 0.5f;
        damage = 1f;
        projectileSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMeleeRotation(0);
    }
}
