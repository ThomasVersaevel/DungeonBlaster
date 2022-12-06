using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : AbstractEnemy
{

    public GameObject swordBox;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        hitpoints = 3;
        attackRange = 1.3f; //1.3 is melee
        attackSpeed = 1.6f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAbstract();

        anim.SetBool("move", true);
    
    }

    public override void MoveToTarget()
    {
        //rb.velocity = playerVector.normalized * ms;
        if (playerDistance < attackRange && !attacking)
        {
            attacking = true;
            attackTimer = attackSpeed;
            anim.SetBool("attack", true);
        }
        
        if (attacking)
        {
            if (attackTimer > 0)
            {
                if (attackTimer < attackSpeed - 0.30)
                {
                    swordBox.SetActive(true); //matches hitbox with animation
                }
                if (attackTimer < attackSpeed - 0.22)
                {
                    anim.SetBool("attack", false);
                }
                attackTimer -= Time.deltaTime;
            } else
            {
                attacking = false;
                swordBox.SetActive(false);
                anim.SetBool("attack", false);
            }
        }
    }
}

