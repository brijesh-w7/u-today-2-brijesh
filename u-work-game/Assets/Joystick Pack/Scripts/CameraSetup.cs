using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    // public Transform kitchenArea;
    public Camera cam;

    public float baseFOV = 60f; // designed for reference aspect
    public float referenceAspect = 16f / 9f;

    void Start()
    {
        AdjustFOV();
    }

    void AdjustFOV()
    {
        // cam = GetComponent<Camera>();

        float currentAspect = (float)Screen.width / Screen.height;

        float baseFOVRad = baseFOV * Mathf.Deg2Rad;

        // Convert vertical FOV -> horizontal FOV (reference)
        float horizontalFOV = 2f * Mathf.Atan(Mathf.Tan(baseFOVRad / 2f) * referenceAspect);

        // Convert back to vertical FOV for current screen
        float newVerticalFOV = 2f * Mathf.Atan(Mathf.Tan(horizontalFOV / 2f) / currentAspect);

        cam.fieldOfView = newVerticalFOV * Mathf.Rad2Deg;
    }
}