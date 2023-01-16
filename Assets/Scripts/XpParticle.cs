using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class XpParticle : MonoBehaviour
{
    SpriteRenderer sr;
    float hue;
    public int xp;
    private bool swirling = false;
    private Vector3 target;
    public float speed;
    private float colorSpeed;
    public float radius = 1f;
    private Light2D light;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        hue = 0;
        speed = 7;
        colorSpeed = 0.1f;
        light = GetComponent<Light2D>();
    }

    void Update()
    {
        // nice swirl to the xp particle
        if ( swirling ) {
            GameObject targetObject = GameObject.FindGameObjectWithTag("Player");
            target = targetObject.transform.position;
            Vector3 direction = target - transform.position;

            // Rotate the direction vector by 90 degrees to create the swirling effect
            direction = Quaternion.Euler(0, 0, 90) * direction;
            direction = direction.normalized * radius; // add circular motion
            Vector3 targetPosition = target;
            if (radius > 0.5f)
            {
                radius -= 1.5f * Time.deltaTime; //reduce circle diameter
                targetPosition = transform.position + direction;
            }
            // Move the game object towards the target position using the rotated direction vector
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, target) < 0.2f)
            {
                targetObject.GetComponent<Player>().GainXP(xp);
                Destroy(gameObject);
            }
        }
       
        hue += Time.deltaTime * colorSpeed;

        if (hue > 1)
        {
            hue = 0.0f;
        }
        // Convert the hue value to RGB and set it as the color of the sprite renderer
        sr.color = Color.HSVToRGB(hue, 0.8f, 1);
        light.color = Color.HSVToRGB(hue, 0.9f, 1);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && !swirling)
        {
            // do the nice swirl to player
            target = coll.gameObject.transform.position;
            swirling = true;
        }
    }
    

}
