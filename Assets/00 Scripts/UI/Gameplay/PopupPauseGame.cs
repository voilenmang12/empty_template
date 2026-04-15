using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPauseGame : UIBase
{
    public Button btnHome, btnRetry;

    private void Start()
    {
        btnHome.onClick.AddListener(OnClickHome);
        btnRetry.onClick.AddListener(OnClickRetry);
    }

    public void OnClickHome()
    {
        GameManager.Instance.GoSceneHome();
    }

    public void OnClickRetry()
    {
        GameManager.Instance.PlayGame(GameManager.Instance.GameType);
    }
}
