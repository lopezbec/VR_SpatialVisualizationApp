using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomSocketInteractor : XRSocketInteractor
{
    // Store the initial rotation of the object
    private Quaternion initialRotation;

    // Override the OnSelectEntering method to store the original rotation before it is changed
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        // Save the initial rotation of the object being interacted with
        initialRotation = args.interactableObject.transform.rotation;

        // Call the base method
        base.OnSelectEntering(args);
    }

    // Override the OnSelectEntered method to reset the rotation to the original
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Call the base method first
        base.OnSelectEntered(args);

        // Preserve the original rotation of the selected object
        args.interactableObject.transform.rotation = initialRotation;
    }
}
