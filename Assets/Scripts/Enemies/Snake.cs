using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : AbstractEnemy
{
    [SerializeField] private GameObject Projectile;
    [SerializeField] private float projectileSpeed;
    // Start is called before the first frame update
    protected override void Start()
    {
        ms = 3f;
        hitpoints = 10;
        attackRange = 5;
        attackSpeed = 2;
        projectileSpeed = 5;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAbstract();
    }

    public override void MoveToTarget()
    {
        CalculatePlayerDistance();
        if (playerDistance > attackRange)
        {
            base.MoveToTarget();
        } else
        {
            if (attackTimer < 0)
            {
                Attack();
                attackTimer = attackSpeed;
            }
        }
        attackTimer -= Time.deltaTime;
    }

    private void Attack()
    {
        GameObject proj = Instantiate(Projectile, transform.position, transform.rotation);
        proj.GetComponent<Rigidbody2D>().velocity = playerVector.normalized * projectileSpeed;
    }
}
