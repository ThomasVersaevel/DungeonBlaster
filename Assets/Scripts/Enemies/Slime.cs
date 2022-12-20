using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : AbstractEnemy
{

    public int stage = 1;
    public GameObject slime;
    private TrailRenderer SlimeTrail;
    public GameObject TrailObject;
    public float size;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
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
            transform.localScale = new Vector3(size, size, size);
        } else if (stage == 2)
        {
            hitpoints = 3;
            //SlimeTrail.startWidth = 0.8f;
            transform.localScale = new Vector3(size-0.5f, size - 0.5f, size - 0.5f);
        } else if (stage == 3)
        {
            hitpoints = 1;
            //SlimeTrail.startWidth = 0.6f;
            transform.localScale = new Vector3(size - 1f, size - 1f, size - 1f);
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
        GameObject childSlime = Instantiate(slime, transform.position-playerVector.normalized, Quaternion.identity);
        GameObject childSlime2 = Instantiate(slime, transform.position-playerVector.normalized, Quaternion.identity);
        childSlime.SendMessage("SetupSlimeStage", stage);
        childSlime2.SendMessage("SetupSlimeStage", stage);
        childSlime.SendMessage("ResetColor");
        childSlime2.SendMessage("ResetColor"); //workaround for the red slime spawning every time
        Destroy(gameObject);
    }
}
