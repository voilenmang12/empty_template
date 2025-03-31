using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeIAPHistoryButton : HomeFeatureButton
{
    public override void OnClick()
    {
        UIManager.Instance.ShowPopupIAPHistory();
    }

    protected override void CheckActive()
    {
    }

    protected override void CheckNoti()
    {
        notiObj.SetActive(IIAPController.Instance.NotiUI());
    }
}
