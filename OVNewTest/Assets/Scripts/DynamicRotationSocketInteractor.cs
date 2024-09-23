using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DynamicRotationSocketInteractor : XRSocketInteractor
{
    private Quaternion originalRotation;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        // Store the original rotation of the interactable object
        if (args.interactableObject != null)
        {
            originalRotation = args.interactableObject.transform.rotation;
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Restore the original rotation after the object is selected
        if (args.interactableObject != null)
        {
            args.interactableObject.transform.rotation = originalRotation;
        }
    }
}
