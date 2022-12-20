using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProjectile : AProjectile
{
    private float rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation += 1000 * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
