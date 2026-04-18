using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class MyCanvasScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasScaler>().matchWidthOrHeight = DeviceTypeChecker.IsTabOrIpad ? 1 : 0;
    }


}
