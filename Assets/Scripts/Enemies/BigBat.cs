using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : AbstractEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        ms = 2;
        rb = gameObject.GetComponent<Rigidbody2D>();
        hitpoints = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAbstract();
    }
}
