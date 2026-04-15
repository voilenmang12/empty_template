using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        HandleTouch(pos);
    }

    void HandleTouch(Vector3 pos)
    {
        DebugCustom.Log($"Touch Pos {pos}");

        GameplayManager.Instance.OnClick(pos);
    }
}