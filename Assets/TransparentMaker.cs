using UnityEngine;

public class TransparentWhenPlayerBehind : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float transparentAlpha = 0.5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Color newColor = originalColor;
            newColor.a = transparentAlpha;
            spriteRenderer.color = newColor;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = originalColor;
        }
    }
}
