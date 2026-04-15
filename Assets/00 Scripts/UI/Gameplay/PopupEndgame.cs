using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupEndgame : UIBase
{
    public TextMeshProUGUI txtShow;
    public CommonButton btnPlay;
    public UiResourceItem resourceItem;
    public Transform itemParent;
    public Button btnCheck, btnReturn;
    public CanvasGroup canvasGroup;

    private void Start()
    {
        btnCheck.onClick.AddListener(OnClickCheck);
        btnReturn.onClick.AddListener(OnClickReturn);
        btnReturn.gameObject.SetActive(false);
        canvasGroup.alpha = 1;
    }

    public override void Show()
    {
        base.Show();
        if (GameplayManager.Instance.PackReward != null)
        {
            foreach (var item in GameplayManager.Instance.PackReward.lstResource)
            {
                UiResourceItem uiItem = Instantiate(resourceItem, itemParent);
                uiItem.InitResouce(item, true);
            }
        }

        txtShow.text = GameplayManager.Instance.winGame ? "Level Win" : "Level Lose";
        btnCheck.gameObject.SetActive(!GameplayManager.Instance.winGame);
        btnPlay.txtVisual.text = GameplayManager.Instance.winGame ? "Next Level" : "Restart";
    }

    public void OnClickPlay()
    {
        GameManager.Instance.PlayGame(GameManager.Instance.GameType);
    }

    public void OnClickHome()
    {
        GameManager.Instance.GoSceneHome();
    }

    public void OnClickCheck()
    {
        canvasGroup.alpha = 0;
        btnReturn.gameObject.SetActive(true);
    }

    public void OnClickReturn()
    {
        btnReturn.gameObject.SetActive(false);
        canvasGroup.alpha = 1;
    }
}