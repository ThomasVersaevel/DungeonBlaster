using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : AbstractEnemy
{

    public int stage = 1;
    public int maxStage;
    public GameObject slime;
    private TrailRenderer SlimeTrail;
    //public GameObject TrailObject;
    public float size;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        SetupSlimeStage(stage);
        gameObject.GetComponent<ParticleSystem>().Play();
    }

    public void SetupSlimeStage(int stage)
    {
        SlimeTrail = transform.GetChild(0).GetComponent<TrailRenderer>();
        if (stage == 1)
        {
            hitpoints = 5;
            SlimeTrail.startWidth = .5f;
            transform.localScale = new Vector3(size, size, size);
        } else if (stage == 2)
        {
            hitpoints = 3;
            SlimeTrail.startWidth = 0.3f;
            transform.localScale = new Vector3(size-0.5f, size - 0.5f, size - 0.5f);
        } else if (stage == 3)
        {
            hitpoints = 1;
            SlimeTrail.startWidth = 0.2f;
            transform.localScale = new Vector3(size - 1f, size - 1f, size - 1f);
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
        if (stage > maxStage)
        {
            //Destroy(TrailObject);
            base.Death();
        }
        else
        {
            GameObject childSlime = Instantiate(slime, transform.position - playerVector.normalized, Quaternion.identity);
            GameObject childSlime2 = Instantiate(slime, transform.position - playerVector.normalized, Quaternion.identity);
            Slime slimeScript = childSlime.GetComponent<Slime>();
            slimeScript.SetupSlimeStage(stage);
            slimeScript.ResetColor();
            Slime slimeScript2 = childSlime2.GetComponent<Slime>();
            slimeScript2.SetupSlimeStage(stage);
            slimeScript2.ResetColor(); //workaround for the red slime spawning every time
            Destroy(gameObject);
        }
    }
}
