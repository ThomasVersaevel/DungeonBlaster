using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject layer1;
    public GameObject layer2;
    public GameObject layer3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, -0.5f);
        layer1.transform.Rotate(0, 0, 1f);
        layer2.transform.Rotate(0, 0, 1.5f);
        layer3.transform.Rotate(0, 0, 0.5f);
    }
}
