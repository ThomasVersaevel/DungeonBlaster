using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ImageRainbow : MonoBehaviour
{
    // The sprite renderer for the sprite that will display the rainbow gradient
    private Image image;
    private Light2D light;
    // The speed at which the rainbow gradient will move
    private float gradientMoveSpeed = 0.5f;

    // The offset for the rainbow gradient
    private float gradientOffset = 0.0f;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        //light = transform.GetChild(0).GetComponent<Light2D>();
    }

    void FixedUpdate()
    {
        // Update the gradient offset based on the movement speed
        gradientOffset += Time.deltaTime * gradientMoveSpeed;

        // If the offset exceeds 1.0, reset it back to 0.0
        if (gradientOffset > 1.0f)
        {
            gradientOffset = 0.0f;
        }

        // Create a new color using the gradient offset and set it as the color of the sprite renderer
        image.color = Color.HSVToRGB(gradientOffset, 0.6f, 1.0f);
        //light.color = Color.HSVToRGB(gradientOffset, 0.9f, 1);
    }
}
