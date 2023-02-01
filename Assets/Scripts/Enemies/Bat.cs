using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBat : AbstractEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        ms = 2.5f;
        hitpoints = 50;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAbstract();
    }
}
