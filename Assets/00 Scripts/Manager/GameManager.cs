using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsTester;
    public bool Initilized { get; private set; }

    public EGameState GameState => _gameState;
    [SerializeField, ReadOnly] EGameState _gameState;
    public EGameType GameType { get; private set; }
    public bool IsEditor { get; private set; }

    private void Start()
    {
        StartCoroutine(IEInit());
    }

    IEnumerator IEInit()
    {
#if UNITY_EDITOR
        IsEditor = true;
#endif
        yield return null;
        yield return new WaitUntil(() => LoadingPanel.Instance);
        yield return new WaitUntil(() => DataSystem.Instance);
        yield return new WaitUntil(() => IAPManager.Instance);

        IAPManager.Instance.InitializePurchasing();

        AchievementController.Instance.Init();
        PlayerResource.Instance.Init();
        PlayerInfoController.Instance.Init();
        GameSettingController.Instance.Init();
        IAPController.Instance.Init();

        TimerController.Instance.Init();
        LoadingPanel.Instance.ShowTextLoading("Loading Complete");
        yield return null;

        AudioManager.Instance.GameInit();
        Initilized = true;
        DoIntervalAction(1, OnTick);
        
        yield return StartCoroutine(LoadingPanel.Instance.EndLoading());
        
        GoSceneHome();
    }

    private void Update()
    {
        if (Initilized)
            TigerForge.EventManager.EmitEventData(Constant.EVENT_TIMER_UPDATE, Time.deltaTime);
    }

    private void OnTick()
    {
        if (Initilized)
            TigerForge.EventManager.EmitEvent(Constant.EVENT_TIMER_TICK);
    }

    public void PlayGame(EGameType gameType)
    {
        if (GameState == EGameState.Loading)
            return;

        GameType = gameType;
        GoSceneGameplay();
    }

    public void GoSceneHome()
    {
        SetState(EGameState.Loading);
        StartCoroutine(SceneHelper.Instance.IEReturnHome());
    }

    void GoSceneGameplay()
    {
        SetState(EGameState.Loading);
        StartCoroutine(SceneHelper.Instance.IEGoGameplay());
    }

    public void SetState(EGameState gameState)
    {
        _gameState = gameState;
    }

    #region Ads

    public void ShowAdsReward(System.Action actionComplete, EAdsRewardPlacement placement)
    {
        actionComplete?.Invoke();
        return;
    }

    public void ShowInterAds(System.Action actionClose, EAdsInterPlacement placement)
    {
        actionClose?.Invoke();
        return;
    }

    #endregion

    public void DoWaitAction(float time, System.Action actionComplete)
    {
        StartCoroutine(IEWaitAction(time, actionComplete));
    }

    IEnumerator IEWaitAction(float time, System.Action actionComplete)
    {
        yield return new WaitForSeconds(time);
        actionComplete?.Invoke();
    }

    public void DoIntervalAction(float time, System.Action actionCallback)
    {
        StartCoroutine(IEIntervalAction(time, actionCallback));
    }

    IEnumerator IEIntervalAction(float time, System.Action actionCallback)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            actionCallback?.Invoke();
        }
    }

}