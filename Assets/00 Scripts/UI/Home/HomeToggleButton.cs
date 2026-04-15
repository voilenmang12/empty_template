using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class HomeToggleButton : MonoBehaviour
{
    public Button btn;
    public GameObject btnOn;
    public Image icon;
    public TextMeshProUGUI txtName;
    public float onAnchorPosY, offAnchorPosY;

    private void Start()
    {
        btn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        AudioManager.Instance.PlaySfx(ESfx.ButtonSfx);
        UIManager.Instance.uIHome.OnClickHomeButton(this);
    }

    public void SetActive(bool isOn)
    {
        btn.interactable = !isOn;
        btnOn.gameObject.SetActive(isOn);
        txtName.DOKill();
        txtName.DOFade(isOn ? 1 : 0f, 0.2f);
        icon.rectTransform.DOKill();
        icon.rectTransform.DOAnchorPosY(isOn ? onAnchorPosY : offAnchorPosY, 0.2f);
        icon.rectTransform.DOScale(isOn ? 1f : 0.8f, 0.2f);
    }
}