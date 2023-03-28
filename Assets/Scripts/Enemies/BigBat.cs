using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBat : AbstractEnemy
{
    private float timer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        ms = 2;
        hitpoints = 40;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 4) // slow down first
        {
            ms = .7f;
        }
        if (timer > 5) // burst of movement speed
        {
            ms = 5.5f;
        }
        if (timer > 6) // reset movement speed
        {
            ms = 2;
            timer = 0;
        }

        UpdateAbstract();
    }
}
