using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTile : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        anim.SetTrigger("Collision");
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        anim.ResetTrigger("Collision");
    }
}
