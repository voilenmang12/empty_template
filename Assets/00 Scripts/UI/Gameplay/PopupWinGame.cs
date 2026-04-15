using System;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class PopupWinGame : UIBase
{
    public Button btnClaim, btnAds, btnNextLevel, btnHome;
    public SkeletonGraphic skeletonGraphic;
    [SpineAnimation] public string animAppear, animIdle;

    private void Start()
    {
        btnClaim.onClick.AddListener(OnClickClaim);
        btnAds.onClick.AddListener(OnClickAds);
        btnNextLevel.onClick.AddListener(OnClickNextLevel);
        btnHome.onClick.AddListener(OnClickHome);
    }

    public override void Show()
    {
        base.Show();
        AudioManager.Instance.PlaySfx(ESfx.WinSfx);
        btnClaim.gameObject.SetActive(true);
        btnAds.gameObject.SetActive(true);

        btnNextLevel.gameObject.SetActive(false);
        btnHome.gameObject.SetActive(false);

        skeletonGraphic.AnimationState.SetAnimation(0, animAppear, false);
        skeletonGraphic.AnimationState.AddAnimation(1, animIdle, true, 0);
    }

    void OnClaimReward()
    {
        btnClaim.gameObject.SetActive(false);
        btnAds.gameObject.SetActive(false);

        btnNextLevel.gameObject.SetActive(true);
        btnHome.gameObject.SetActive(true);
    }

    void OnClickClaim()
    {
        PackageResource package = new PackageResource();
        package.AddResource(new CommonResource(ECommonResource.Coin, 100));
        package.ReceiveAndShow(EResourceFrom.GameDrop, () => { OnClaimReward(); });
    }

    void OnClickAds()
    {
        GameManager.Instance.ShowAdsReward(() =>
        {
            PackageResource package = new PackageResource();
            package.AddResource(new CommonResource(ECommonResource.Coin, 200));
            package.ReceiveAndShow(EResourceFrom.GameDrop, () => { OnClaimReward(); });
        }, EAdsRewardPlacement.X2GameCoin);
    }

    void OnClickHome()
    {
        GameManager.Instance.GoSceneHome();
    }

    void OnClickNextLevel()
    {
        GameManager.Instance.PlayGame(GameManager.Instance.GameType);
    }
}