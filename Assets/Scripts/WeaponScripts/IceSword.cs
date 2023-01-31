using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSword : AWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void UpdateRotation()
    {

    }

    // Instantiate projectile
    // whip sword 
    public override void Shoot(Vector3 mousePos)
    {
        int angleOffset = 8;
        base.Shoot(Quaternion.AngleAxis(Random.Range(-angleOffset, angleOffset), Vector3.back) * mousePos);
    }
}
