using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Services.LevelPlay;
public class AdsManager : Singleton<AdsManager>
{
    static string uniqueUserId = "demoUserUnity";
    LevelPlayBannerAd bannerAd;

#if UNITY_ANDROID
    string appKey = "85460dcd";
    string bannerAdUnitId = "thnfvcsog13bhn08";
    string interstitialAdUnitId = "aeyqi3vqlv6o8sh9";
#elif UNITY_IPHONE
    string appKey = "8545d445";
    string bannerAdUnitId = "iep3rxsyp9na3rw8";
	string interstitialAdUnitId = "wmgt0712uuux8ju4";
#else
    readonly string appKey = "unexpected_platform";
    readonly string bannerAdUnitId = "unexpected_platform";
    string interstitialAdUnitId = "unexpected_platform";
    readonly string interstitialAdUnitId = "unexpected_platform";
#endif

    public static string INTERSTITIAL_INSTANCE_ID = "0";
    public static string REWARDED_INSTANCE_ID = "0";
    int userTotalCredits = 0;

    LevelPlayInterstitialAd interstitialAd;
    void Start()
    {
        Debug.Log("unity-script: Awake called");

        //Dynamic config example
        IronSourceConfig.Instance.setClientSideCallbacks(true);

        var id = IronSource.Agent.getAdvertiserId();
        Debug.Log("unity-script: IronSource.Agent.getAdvertiserId : " + id);

        Debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        Debug.Log("unity-script: unity version" + IronSource.unityVersion());

        // SDK init
        Debug.Log("unity-script: LevelPlay Init");
        LevelPlay.Init(appKey, uniqueUserId, new[]
        {
            com.unity3d.mediation.LevelPlayAdFormat.REWARDED,
            com.unity3d.mediation.LevelPlayAdFormat.INTERSTITIAL,
            com.unity3d.mediation.LevelPlayAdFormat.BANNER
        });

        LevelPlay.OnInitSuccess += OnInitializationCompleted;
        LevelPlay.OnInitFailed += error => Debug.Log("Initialization error: " + error);
    }

    void OnInitializationCompleted(LevelPlayConfiguration configuration)
    {
        Debug.Log("Initialization completed");
        LoadBanner();
        LoadInterstitial();
        InitRewardVideo();
    }

    #region Banner
    void LoadBanner()
    {
        // Create the banner object
        bannerAd = new LevelPlayBannerAd(bannerAdUnitId);

        bannerAd.OnAdLoaded += BannerOnAdLoadedEvent;
        bannerAd.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
        bannerAd.OnAdDisplayed += BannerOnAdDisplayedEvent;
        bannerAd.OnAdDisplayFailed += BannerOnAdDisplayFailedEvent;
        bannerAd.OnAdClicked += BannerOnAdClickedEvent;
        bannerAd.OnAdCollapsed += BannerOnAdCollapsedEvent;
        bannerAd.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
        bannerAd.OnAdExpanded += BannerOnAdExpandedEvent;

        // Ad load
        bannerAd.LoadAd();
    }

    void HideBanner()
    {
        if (bannerAd != null)
        {
            bannerAd.HideAd();
            bannerAd.OnAdLoaded -= BannerOnAdLoadedEvent;
            bannerAd.OnAdLoadFailed -= BannerOnAdLoadFailedEvent;
            bannerAd.OnAdDisplayed -= BannerOnAdDisplayedEvent;
            bannerAd.OnAdDisplayFailed -= BannerOnAdDisplayFailedEvent;
            bannerAd.OnAdClicked -= BannerOnAdClickedEvent;
            bannerAd.OnAdCollapsed -= BannerOnAdCollapsedEvent;
            bannerAd.OnAdLeftApplication -= BannerOnAdLeftApplicationEvent;
            bannerAd.OnAdExpanded -= BannerOnAdExpandedEvent;
        }
    }
    //Banner Events
    void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdLoadedEvent With AdInfo " + adInfo);
    }

    void BannerOnAdLoadFailedEvent(LevelPlayAdError error)
    {
        Debug.Log("unity-script: I got BannerOnAdLoadFailedEvent With Error " + error);
    }

    void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdClickedEvent With AdInfo " + adInfo);
    }

    void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdDisplayedEvent With AdInfo " + adInfo);
    }

    void BannerOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError adInfoError)
    {
        Debug.Log("unity-script: I got BannerOnAdDisplayFailedEvent With AdInfoError " + adInfoError);
    }

    void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdCollapsedEvent With AdInfo " + adInfo);
    }

    void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdLeftApplicationEvent With AdInfo " + adInfo);
    }

    void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdExpandedEvent With AdInfo " + adInfo);
    }

    #endregion

    #region Interstitial
    Action actionInterSuccess, actionInterFail;
    public void LoadInterstitial()
    {
        // Create interstitial Ad
        interstitialAd = new LevelPlayInterstitialAd(interstitialAdUnitId);

        // Register to events
        interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
        interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
        interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
        interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
        interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
        interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
        interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;

        Debug.Log("unity-script: LoadInterstitial");
        interstitialAd.LoadAd();
    }

    public void ShowInterstitial(string placement, Action actionSuccess, Action actionFail = null)
    {
        actionInterSuccess = actionSuccess;
        actionInterFail = actionFail;

        Debug.Log("unity-script: ShowInterstitialButtonClicked");
        if (interstitialAd.IsAdReady())
            interstitialAd.ShowAd(placement);
        else
        {
            Debug.Log("unity-script: interstitialAd.IsAdReady - False");
            OnAdsInterCallBack(false);
        }
    }
    void OnAdsInterCallBack(bool success)
    {
        if (success)
        {
            actionInterSuccess?.Invoke();
        }
        else
        {
            actionInterFail?.Invoke();
        }
        actionInterSuccess = null;
        actionInterFail = null;
    }
    void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdLoadedEvent With AdInfo " + adInfo);
    }

    void InterstitialOnAdLoadFailedEvent(LevelPlayAdError error)
    {
        Debug.Log("unity-script: I got InterstitialOnAdLoadFailedEvent With Error " + error);
    }

    void InterstitialOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdDisplayedEvent With AdInfo " + adInfo);
    }

    void InterstitialOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError infoError)
    {
        Debug.Log("unity-script: I got InterstitialOnAdDisplayFailedEvent With InfoError " + infoError);
        OnAdsInterCallBack(false);
    }

    void InterstitialOnAdClickedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdClickedEvent With AdInfo " + adInfo);
    }

    void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdClosedEvent With AdInfo " + adInfo);
        OnAdsInterCallBack(true);
    }

    void InterstitialOnAdInfoChangedEvent(LevelPlayAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdInfoChangedEvent With AdInfo " + adInfo);
    }
    #endregion

    #region RewardVideo
    System.Action actionRewardSuccess, actionRewardFail;
    bool isRewardSuccess = false;
    void InitRewardVideo()
    {
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
    }
    public void ShowRewardedVideo(string placement, Action actionSuccess, Action actionFail = null)
    {
        isRewardSuccess = false;
        actionRewardSuccess = actionSuccess;
        actionRewardFail = actionFail;

        Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
        if (IronSource.Agent.isRewardedVideoAvailable())
            IronSource.Agent.showRewardedVideo(placement);
        else
        {
            Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
            OnAdsRewardCallBack(false);
        }
    }

    void OnAdsRewardCallBack(bool success)
    {
        if (success && isRewardSuccess)
        {
            actionRewardSuccess?.Invoke();
        }
        else
        {
            actionRewardFail?.Invoke();
        }
        isRewardSuccess = false;
        actionRewardSuccess = null;
        actionRewardFail = null;
    }

    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdOpenedEvent With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdClosedEvent With AdInfo " + adInfo);
        OnAdsRewardCallBack(true);
    }

    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdAvailable With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdUnavailable()
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdUnavailable");
    }

    void RewardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent With Error" + ironSourceError + "And AdInfo " + adInfo);
        OnAdsRewardCallBack(false);
    }

    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
        isRewardSuccess = true;
    }

    void RewardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
    }
    #endregion
    void OnDestroy()
    {
        bannerAd?.DestroyAd();
        interstitialAd?.DestroyAd();
    }
}
