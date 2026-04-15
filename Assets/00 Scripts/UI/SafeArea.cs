using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    public Transform uiParent;
    void Start()
    {
        ApplySafeArea();
        if(uiParent == null)
            uiParent = transform;
        UIManager.Instance.uISafeZone = this;
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        RectTransform rt = GetComponent<RectTransform>();
        if (rt == null) return;

        // Determine the actual screen resolution used by the canvas
        float screenW = Screen.width;
        float screenH = Screen.height;

        // Convert safe-area pixel rect to normalized anchor values [0,1]
        // Vector2 anchorMin = new Vector2(safeArea.xMin / screenW, safeArea.yMin / screenH);
        Vector2 anchorMax = new Vector2(safeArea.xMax / screenW, safeArea.yMax / screenH);

#if UNITY_IOS
        // anchorMin /= 2;
        anchorMax = (anchorMax + Vector2.one) / 2;        
#endif
        // rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        

        // Reset offsets so the rect exactly follows the anchors
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // DebugCustom.LogColor($"SafeArea applied — anchorMin:{anchorMin} anchorMax:{anchorMax}", gameObject.name);
    }
}
