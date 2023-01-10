using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowGradient : MonoBehaviour
{
    // The sprite renderer for the sprite that will display the rainbow gradient
    public SpriteRenderer spriteRenderer;

    // The speed at which the rainbow gradient will move
    public float gradientMoveSpeed = 0.3f;

    // The offset for the rainbow gradient
    private float gradientOffset = 0.0f;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Update the gradient offset based on the movement speed
        gradientOffset += Time.deltaTime * gradientMoveSpeed;

        // If the offset exceeds 1.0, reset it back to 0.0
        if (gradientOffset > 1.0f)
        {
            gradientOffset = 0.0f;
        }

        // Create a new color using the gradient offset and set it as the color of the sprite renderer
        spriteRenderer.color = Color.HSVToRGB(gradientOffset, 1.0f, 1.0f);
    }
}
