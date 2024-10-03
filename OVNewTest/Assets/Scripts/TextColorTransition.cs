using UnityEngine;
using TMPro;

public class TextColorTransition : MonoBehaviour
{
    public Color targetColor = Color.red; // The color to transition to
    public float transitionDuration = 1.0f; // Duration of the color transition

    private TextMeshPro textMeshPro;
    private Color originalColor;
    private bool isTransitioning = false;
    private float transitionProgress = 0f;

    void Start()
    {
        // Get the TextMeshPro component attached to this GameObject
        textMeshPro = GetComponent<TextMeshPro>();
        if (textMeshPro != null)
        {
            originalColor = textMeshPro.color;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            isTransitioning = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset the color when the player exits the trigger
        if (other.CompareTag("Player"))
        {
            isTransitioning = true;
            targetColor = originalColor;
            transitionProgress = 0f; // Reset progress for a smooth transition back
        }
    }

    void Update()
    {
        if (isTransitioning && textMeshPro != null)
        {
            // Progressively change the color over time
            transitionProgress += Time.deltaTime / transitionDuration;
            textMeshPro.color = Color.Lerp(textMeshPro.color, targetColor, transitionProgress);

            // Stop the transition when complete
            if (transitionProgress >= 1f)
            {
                isTransitioning = false;
            }
        }
    }
}
