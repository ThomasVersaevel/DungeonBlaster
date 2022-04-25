using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("End", 3f);
        gameObject.GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void End()
    {
        Destroy(gameObject);
    }
}
