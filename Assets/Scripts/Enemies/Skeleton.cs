using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : AbstractEnemy
{

    public GameObject swordBox;
    private Animator anim;

    // Start is called before the first frame update
    protected override void Start()
    {
        ms = 2;
        anim = gameObject.GetComponent<Animator>();
        hitpoints = 3;
        attackRange = 1.3f; //1.3 is melee
        attackSpeed = 1.6f;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAbstract();
        
        anim.SetBool("move", true);
    
    }

    public override void MoveToTarget()
    {
        Vector2 moveDir = playerVector.normalized * ms * Time.deltaTime;
        transform.position += new Vector3(moveDir.x, moveDir.y, 0);

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

