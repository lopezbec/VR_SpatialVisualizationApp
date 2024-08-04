using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandMenuToggle : MonoBehaviour
{
    public GameObject handMenu; // The hand menu to toggle
    public InputActionReference toggleActionReference; // Input action reference for the Y button

    private void OnEnable()
    {
        toggleActionReference.action.performed += OnToggleAction;
    }

    private void OnDisable()
    {
        toggleActionReference.action.performed -= OnToggleAction;
    }

    private void OnToggleAction(InputAction.CallbackContext context)
    {
        handMenu.SetActive(!handMenu.activeSelf);
    }
}
