using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCosmetic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
