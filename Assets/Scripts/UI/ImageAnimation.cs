using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{

    public Sprite[] sprites;
    public float animationTime;
    public bool loop = true;
    public bool destroyOnEnd = false;

    private int index = 0;
    private Image image;
    private float timer = 0;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (!loop && index == sprites.Length) return;

        timer += Time.deltaTime;
        if (timer < animationTime) return;
        image.sprite = sprites[index];
        timer = 0;
        index++;
        if (index >= sprites.Length)
        {
            if (loop) index = 0;
            if (destroyOnEnd) Destroy(gameObject);
        }
    }
}