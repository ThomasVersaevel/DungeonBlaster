using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : AbstractEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        ms = 2;
        hitpoints = 1;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAbstract();
    }
}
