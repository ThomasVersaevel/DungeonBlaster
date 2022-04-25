using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : AbstractEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        hitpoints = 5;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAbstract();
    }
}
