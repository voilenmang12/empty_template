using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeHackButton : HomeFeatureButton
{
    int count = 0;
    public override void OnClick()
    {
        count++;
        if (count % 2 == 1)
            AdsManager.Instance.ShowRewardedVideo("ad_reward", RewardSuccess,
                () =>
                {
                    UIManager.Instance.ShowDialog("Show Ad Reward Fail");
                });
        else
            AdsManager.Instance.ShowInterstitial("ad_inter", RewardSuccess,
                () =>
                {
                    UIManager.Instance.ShowDialog("Show Ad Inter Fail");
                });
    }
    void RewardSuccess()
    {
        PackageResource package = new PackageResource();
        package.AddResource(new CommonResource(ECommonResource.Gem, 50));
        package.AddResource(new CommonResource(ECommonResource.Energy, 5));
        package.AddResource(new CommonResource(ECommonResource.Coin, 500));
        package.ReceiveAndShow(EResourceFrom.Hack);
    }
    protected override void CheckActive()
    {
        bool active = GameManager.Instance.BuildType == EBuildType.Dev;
#if UNITY_EDITOR
        active = true;
#endif
        gameObject.SetActive(active);
    }

    protected override void CheckNoti()
    {
    }
}
