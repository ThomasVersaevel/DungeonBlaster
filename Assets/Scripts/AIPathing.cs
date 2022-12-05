using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// written by openai
public class AIPathing : MonoBehaviour
{
    // The target position that the game object will move towards
    [SerializeField]
    private Transform target;

    // The speed at which the game object will move
    public float speed = 1f;

    // The layer mask for the obstacles that the game object will avoid
    public LayerMask obstacleMask;

    // The minimum distance that the game object will maintain from the obstacles
    public float obstacleAvoidanceDistance = 0.5f;

    // The maximum angle that the game object can turn in a single frame
    public float maxTurnAngle = 180f;

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Calculate the direction from the current position to the target position
        Vector2 direction = target.position - transform.position;

        // Calculate the angle between the current forward direction of the game object and the direction to the target
        float angle = Vector2.Angle(transform.forward, direction);

        //// Clamp the angle to the maximum turn angle
        //angle = Mathf.Min(angle, maxTurnAngle);

        //// Rotate the game object towards the target by the calculated angle
        //transform.Rotate(0, 0, angle);

        // Raycast to check if there are any obstacles in the direction of the target
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, obstacleAvoidanceDistance, obstacleMask);
        if (hit.collider != null)
        {
            print("Obstacle dtected");
            // If an obstacle is detected, move in the opposite direction
            transform.position -= transform.forward * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else
        {
            // If there are no obstacles, move towards the target
            transform.position += transform.forward * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}

