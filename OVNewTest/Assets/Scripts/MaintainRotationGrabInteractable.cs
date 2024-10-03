using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MaintainRotationGrabInteractable : XRGrabInteractable
{
    private Quaternion originalRotation;
    private Transform interactorTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Store the original rotation
        originalRotation = transform.rotation;

        // Store the interactor's transform
        interactorTransform = args.interactorObject.transform;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Restore the original rotation
        transform.rotation = originalRotation;

        // Clear the interactor's transform
        interactorTransform = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected && interactorTransform != null)
        {
            // Update the position to follow the interactor's position
            transform.position = interactorTransform.position;

            // Maintain the original rotation
            transform.rotation = originalRotation;
        }
    }
}
