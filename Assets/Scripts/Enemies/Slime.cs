using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : AbstractEnemy
{

    public int stage = 1;
    public GameObject slime;
    private TrailRenderer SlimeTrail;
    public GameObject TrailObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        SetupSlimeStage(stage);
        gameObject.GetComponent<ParticleSystem>().Play();
        SlimeTrail = TrailObject.GetComponent<TrailRenderer>();
    }

    public void SetupSlimeStage(int stage)
    {
        if (stage == 1)
        {
            hitpoints = 5;
            //SlimeTrail.startWidth = 1;
            transform.localScale = new Vector3(2, 2, 1);
        } else if (stage == 2)
        {
            hitpoints = 3;
            //SlimeTrail.startWidth = 0.8f;
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        } else if (stage == 3)
        {
            hitpoints = 1;
            //SlimeTrail.startWidth = 0.6f;
            transform.localScale = new Vector3(1f, 1f, 1);
        } else
        {
            base.Death();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAbstract();

    }

    public override void Death() //activate particles, spawn two smaller slimes, destroy big slime
    {
        stage++;
        GameObject childSlime = Instantiate(slime, transform.position, Quaternion.identity);
        GameObject childSlime2 = Instantiate(slime, transform.position, Quaternion.identity);
        childSlime.SendMessage("SetupSlimeStage", stage);
        childSlime2.SendMessage("SetupSlimeStage", stage);
        childSlime.SendMessage("ResetColor");
        childSlime2.SendMessage("ResetColor"); //workaround for the red slime spawning every time
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("TakeDamage");
        }
    }
}
