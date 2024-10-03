using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI; // Include this if using Unity UI Text
using TMPro; // Include this if using TextMeshPro
using System.Collections;

public class GlowOnTrigger : MonoBehaviour
{
    public Material initialGlowMaterial; // Assign the glow material in the Inspector
    public Color targetGlowColor = Color.white;
    public Color targetTextColor = Color.yellow; // New field for target text color
    public float transitionTime = 1.0f;

    private Material initialMaterial;
    private Color initialColor;
    private Renderer objectRenderer;
    private Coroutine currentCoroutine;

    // References to the text components
    public Text uiText; // If using Unity UI Text
    public TextMeshProUGUI tmpText; // If using TextMeshPro

    private Color initialTextColor;

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

        // Initialize the initial text color if the text component is assigned
        if (uiText != null)
        {
            initialTextColor = uiText.color;
        }
        else if (tmpText != null)
        {
            initialTextColor = tmpText.color;
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
                currentCoroutine = StartCoroutine(ChangeMainColor(targetGlowColor, targetTextColor, transitionTime));
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

            currentCoroutine = StartCoroutine(ChangeMainColor(initialColor, initialTextColor, transitionTime));
        }
    }

    private bool IsPlayer(Collider other)
    {
        bool isPlayer = other.GetComponent<XRController>() != null || other.CompareTag("Player");
        Debug.Log("IsPlayer check: " + isPlayer);
        return isPlayer;
    }

    private IEnumerator ChangeMainColor(Color targetObjectColor, Color targetTextColor, float duration)
    {
        Color currentObjectColor = objectRenderer.material.color;
        Color currentTextColor = uiText != null ? uiText.color : (tmpText != null ? tmpText.color : Color.white);
        Debug.Log("Changing main color from: " + currentObjectColor + " to: " + targetObjectColor);
        Debug.Log("Changing text color from: " + currentTextColor + " to: " + targetTextColor);
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;

            // Lerp the object color
            objectRenderer.material.color = Color.Lerp(currentObjectColor, targetObjectColor, t);

            // Lerp the text color
            if (uiText != null)
            {
                uiText.color = Color.Lerp(currentTextColor, targetTextColor, t);
            }
            else if (tmpText != null)
            {
                tmpText.color = Color.Lerp(currentTextColor, targetTextColor, t);
            }

            time += Time.deltaTime;
            yield return null;
        }

        objectRenderer.material.color = targetObjectColor;
        Debug.Log("Final main color set to: " + targetObjectColor);

        // Set the final text color
        if (uiText != null)
        {
            uiText.color = targetTextColor;
        }
        else if (tmpText != null)
        {
            tmpText.color = targetTextColor;
        }

        // Revert to the initial material only after the color transition is complete
        if (targetObjectColor == initialColor)
        {
            objectRenderer.material = initialMaterial;
        }
    }
}
