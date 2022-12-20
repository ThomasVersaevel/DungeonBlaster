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
        //if (Input.GetMouseButtonDown(0) && attackTimer < 0)
        //{   // if shooting move the knife in hand to simulate throw

        //    attackAnim = true;
        //    Vector3 mousePos = Input.mousePosition;
        //    Shoot(mousePos);
        //}
        //if (!attackAnim)
        //{
        //    UpdateMeleeRotation(0);
        //} else
        //{
        //    transform.rotation = Quaternion.Euler(0, rotateAmount, 0) * ;
        //}

    }
    //public override void Shoot(Vector3 mousePos)
    //{
    //    rotation += 500 * Time.deltaTime;
    //    gameObject.transform.rotation = Quaternion.Euler(0, 0, rotation);

    //    base.Shoot(mousePos);
    //}
}
