using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBat : AbstractEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        ms = 2.5f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        hitpoints = 50;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAbstract();
    }
}
