using UnityEngine;

public class BillboardYAxis : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
        transform.localEulerAngles = cam.localEulerAngles;
    }
}