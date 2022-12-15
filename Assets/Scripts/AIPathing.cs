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
    public float obstacleDetectionRadius = 0.2f;

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
        //float angle = Vector2.Angle(transform.forward, direction);

        //// Clamp the angle to the maximum turn angle
        //angle = Mathf.Min(angle, maxTurnAngle);

        //// Rotate the game object towards the target by the calculated angle
        //transform.Rotate(0, 0, angle);

        // Raycast to check if there are any obstacles in the direction of the target
        //RaycastHit2D hit = Physics2D.CircleCast(transform.position, obstacleDetectionRadius, direction, obstacleDetectionRadius, obstacleMask);
        //if (hit.collider != null)
        //{
        //    print("Obstacle detected");
        //    // If an obstacle is detected, move around it
        //    Vector2 newDirection = Quaternion.Euler(0, 0, 90) * direction;
        //    direction = Vector2.Lerp(direction, newDirection, 0.5f);
        //}

        // If there are no obstacles, move towards the target
        Vector2 moveDir = direction.normalized * speed * Time.deltaTime;
        transform.position += new Vector3(moveDir.x, moveDir.y, 0);
    }
}

