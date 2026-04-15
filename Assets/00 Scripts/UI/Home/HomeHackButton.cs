using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeHackButton : HomeFeatureButton
{
    public override void OnClick()
    {
        PackageResource package = new PackageResource();
        package.AddResource(new CommonResource(ECommonResource.Gem, 50));
        package.AddResource(new CommonResource(ECommonResource.Energy, 5));
        package.AddResource(new CommonResource(ECommonResource.Coin, 500));
        package.ReceiveAndShow(EResourceFrom.Hack);
    }

    protected override void CheckActive()
    {
        gameObject.SetActive(GameManager.Instance.IsEditor);
    }

    protected override void CheckNoti()
    {
    }
}
