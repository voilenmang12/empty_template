using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UiResourceItem : MonoBehaviour
{
    public Image icon, bg, border;
    public TextMeshProUGUI txtCount;
    public GameObject fxGlow;
    public Button btn;
    GameResource visualResource;
    public void InitResouce(GameResource resource, bool effectAppear = false, bool interactable = true)
    {
        visualResource = resource;
        if (visualResource == null || !visualResource.VisualReward())
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        icon.sprite = visualResource.GetIcon();
        bg.sprite = visualResource.GetBG();
        border.sprite = visualResource.GetBorder();
        btn.interactable = interactable;
        if (effectAppear)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.25f).SetUpdate(UpdateType.Normal, true).OnComplete(() =>
            {
                fxGlow.SetActive(true);
            });
        }
        txtCount.text = visualResource.GetTextValue();
    }
    public void OnClick()
    {
        
    }
}
