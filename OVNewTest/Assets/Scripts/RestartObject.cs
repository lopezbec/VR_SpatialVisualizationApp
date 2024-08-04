using UnityEngine;
using System.Collections.Generic;

public class ResetPositionAndSpeed : MonoBehaviour
{
    private class InitialState
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Velocity;
        public Vector3 AngularVelocity;
    }

    private Dictionary<Transform, InitialState> initialStates = new Dictionary<Transform, InitialState>();

    void Start()
    {
        StoreInitialStates(transform);
    }

    private void StoreInitialStates(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            InitialState state = new InitialState
            {
                Position = child.position,
                Rotation = child.rotation,
                Velocity = rb != null ? rb.velocity : Vector3.zero,
                AngularVelocity = rb != null ? rb.angularVelocity : Vector3.zero
            };

            initialStates[child] = state;

            // Recursively store initial states for all children
            StoreInitialStates(child);
        }
    }

    public void ResetToInitial()
    {
        foreach (var kvp in initialStates)
        {
            Transform child = kvp.Key;
            InitialState state = kvp.Value;

            child.position = state.Position;
            child.rotation = state.Rotation;

            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = state.Velocity;
                rb.angularVelocity = state.AngularVelocity;
            }
        }

        Debug.Log("All positions and Rigidbody properties reset.");
    }
}
