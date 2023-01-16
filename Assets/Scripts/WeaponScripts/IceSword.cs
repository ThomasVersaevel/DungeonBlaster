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
    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
    }
}
