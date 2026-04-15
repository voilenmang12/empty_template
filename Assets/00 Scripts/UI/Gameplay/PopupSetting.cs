using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : UIBase
{
    public RectTransform bgRectTransform;
    public Button btnRestorePurchase, btnHelp;
    public float fullRectY, collapseRectY;

    private void Start()
    {
        btnRestorePurchase.onClick.AddListener(OnClickRestorePurchase);
        btnHelp.onClick.AddListener(OnClickHelp);
        bool ios = false;
#if UNITY_IOS || UNITY_MACOS
        ios = true;
#endif
        if (ios)
        {
            btnRestorePurchase.gameObject.SetActive(true);
            bgRectTransform.sizeDelta = new Vector2(bgRectTransform.sizeDelta.x, fullRectY);
        }
        else
        {
            btnRestorePurchase.gameObject.SetActive(false);
            bgRectTransform.sizeDelta = new Vector2(bgRectTransform.sizeDelta.x, collapseRectY);
        }
    }

    void OnClickRestorePurchase()
    {
        IAPManager.Instance.RestorePurchasing();
    }

    void OnClickHelp()
    {
    }
}