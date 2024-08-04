using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

// DESCRIPTION
/*
This script changes the material color of an object without modifying the shared material itself.
When the player enters the trigger zone, the object's material color changes.
When the player exits the trigger zone, the object's material color reverts back to its original color.
The color transition is smooth and fades between the colors.
*/
public class GlowOnTrigger : MonoBehaviour
{
    public Material initialGlowMaterial; // Assign the glow material in the Inspector
    public Color targetGlowColor = Color.white;
    public float transitionTime = 1.0f;

    private Material initialMaterial;
    private Color initialColor;
    private Renderer objectRenderer;
    private Coroutine currentCoroutine;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            initialMaterial = objectRenderer.material;
            initialColor = initialMaterial.color;
            Debug.Log("Initial material and color set: " + initialColor);
        }
        else
        {
            Debug.LogError("No Renderer component found on this GameObject.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            Debug.Log("Player entered trigger zone.");
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                Debug.Log("Stopped existing coroutine.");
            }

            if (initialGlowMaterial != null)
            {
                objectRenderer.material = new Material(initialGlowMaterial); // Use a new instance of the glow material
                currentCoroutine = StartCoroutine(ChangeMainColor(targetGlowColor, transitionTime));
            }
            else
            {
                Debug.LogError("Glow material is not assigned.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            Debug.Log("Player exited trigger zone.");
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                Debug.Log("Stopped existing coroutine.");
            }

            currentCoroutine = StartCoroutine(ChangeMainColor(initialColor, transitionTime));
        }
    }

    private bool IsPlayer(Collider other)
    {
        bool isPlayer = other.GetComponent<XRController>() != null || other.CompareTag("Player");
        Debug.Log("IsPlayer check: " + isPlayer);
        return isPlayer;
    }

    private IEnumerator ChangeMainColor(Color targetColor, float duration)
    {
        Color currentColor = objectRenderer.material.color;
        Debug.Log("Changing main color from: " + currentColor + " to: " + targetColor);
        float time = 0;

        while (time < duration)
        {
            objectRenderer.material.color = Color.Lerp(currentColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        objectRenderer.material.color = targetColor;
        Debug.Log("Final main color set to: " + targetColor);

        // Revert to the initial material only after the color transition is complete
        if (targetColor == initialColor)
        {
            objectRenderer.material = initialMaterial;
        }
    }
}
