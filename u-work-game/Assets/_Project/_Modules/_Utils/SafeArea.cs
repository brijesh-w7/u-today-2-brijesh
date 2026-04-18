using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaTopOnly : MonoBehaviour
{
    private RectTransform panel;
    private Rect lastSafeArea;
    private Vector2Int lastScreenSize;

    void Awake()
    {
        panel = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    void Update()
    {
        if (Screen.safeArea != lastSafeArea ||
            Screen.width != lastScreenSize.x ||
            Screen.height != lastScreenSize.y)
        {
            ApplySafeArea();
        }
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;

        lastSafeArea = safeArea;
        lastScreenSize = new Vector2Int(Screen.width, Screen.height);

        // Convert safe area top to normalized anchor
        float top = (safeArea.y + safeArea.height) / Screen.height;

        // Apply only TOP constraint
        panel.anchorMin = new Vector2(0, 0);   // keep bottom full
        panel.anchorMax = new Vector2(1, top); // adjust only top
    }
}