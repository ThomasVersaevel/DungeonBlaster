﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    private Color ogColor;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private bool isMoving;
    private float delay = 0.4f;
    private float delayCountdown;
    private float moveTime = 2f;
    private float movementTimer;
    protected Vector3 direction;
    //AIPathing aiPath;

    public float ms;
    public float hitpoints = 3;
    public float attackRange;
    public float attackSpeed;
    protected float attackTimer;

    protected float playerDistance;
    protected Vector3 playerVector;
    protected bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        delayCountdown = delay;
        movementTimer = moveTime;
        ResetColor();
        //aiPath = gameObject.GetComponent<AIPathing>();
    }
    public void UpdateAbstract()
    {
        if (rb.velocity.x > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        if (hitpoints <= 0)
        {
            Death();
        }

    }

    public float CalculatePlayerDistance()
    {
        playerVector = GameObject.Find("Player").transform.position - transform.position;
        playerDistance = Mathf.Sqrt(Mathf.Pow(playerVector.x, 2) + Mathf.Pow(playerVector.y, 2));
        return playerDistance;
    }

    public virtual void MoveToTarget()
    {
        // all done by AIPath
        // rb.velocity = playerVector.normalized * ms;
        // direction = playerVector; //this is for sprite rotation
    }

    void FlashRed()
    {
        sr.color = Color.red;
        Invoke("ResetColor", 0.3f);
    }
    void ResetColor()
    {
        sr.color = Color.white;
    }

    public void TakeDamage(float damage)
    { //method invoked by bullets and sort
        hitpoints -= damage;
        FlashRed();     
    }

    public virtual void Death()
    {
        GameObject.Find("EventSystem").GetComponent<EventSystem>().MakeDeathParticles(transform.position);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        { //when hitting player, already handled in player
            //coll.rigidbody.AddForce(new Vector2(1, 1));
        } else if (coll.gameObject.tag == "Tile")
        { //when hitting wall
            // let aipathing handle this
            //rb.velocity = new Vector2(2, 2);
        } else if (coll.gameObject.tag == "Projectile")
        {
            //already handled in the projectile itself.
        }
    }

}