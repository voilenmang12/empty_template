using System;
using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class GameplayManager : Singleton<GameplayManager>
{
    public EGamePlayState State => state;
    [SerializeField, ReadOnly] protected EGamePlayState state;
    public EGamePlayState LastState { get; private set; }
    public bool winGame { get; private set; }
    public int LevelId { get; set; }
    public PackageResource PackReward { get; private set; }

    public bool GameEnded { get; private set; }
    public bool Initialized { get; private set; }

    private bool showedContinue;


    public IEnumerator IEInit()
    {
        DebugCustom.LogColor("Init Level");
        SetState(EGamePlayState.Cinematic);

        AchievementController.Instance.UpdateAchievementProgress(EAchievementType.LevelPlay);
        PlayerInfoController.Instance.SetPlayed();
        LevelId = AchievementController.Instance.GetAchievementProgress(EAchievementType.LevelCompleted) + 1;
        yield return new WaitUntil(() => ResolutionManager.Instance);
        yield return new WaitUntil(() => ResolutionManager.Instance.Initialized);



        yield return new WaitUntil(() => UIManager.Instance.uIGameplay);
        UIManager.Instance.uIGameplay.Initialize();

        Initialized = true;
        TigerForge.EventManager.StartListening(Constant.EVENT_TIMER_TICK, OnTick);
        TigerForge.EventManager.EmitEvent(Constant.EVENT_LEVEL_INITED);
    }

    public void StartGame()
    {
        SetState(EGamePlayState.Running);
        CheckTut();
    }

    void CheckTut()
    {
        
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < 10; i++)
            {
                OnTick();
            }
        }
#endif
    }
    
    void OnTick()
    {
        
    }

    public void SetState(EGamePlayState _state)
    {
        state = _state;
        DebugCustom.LogColor("GamePlayState", State);
        if (state != EGamePlayState.Pause)
        {
            LastState = state;
            Time.timeScale = 1;
        }
        else
            Time.timeScale = 0;

        TigerForge.EventManager.EmitEvent(Constant.ON_GAME_STATE_CHANGE);
    }

    public void EndGame(bool win)
    {
        if (!win && !showedContinue)
        {
            UIManager.Instance.ShowPopupContinue();
            showedContinue = true;
            return;
        }

        GameEnded = true;
        DebugCustom.LogColor("End Game");
        winGame = win;
        DoWaitAction(1, () =>
        {
            if (winGame)
            {
                AchievementController.Instance.UpdateAchievementProgress(EAchievementType.LevelCompleted);
                UIManager.Instance.ShowPopupWinGame();
            }
            else
            {
                AchievementController.Instance.UpdateAchievementProgress(EAchievementType.LevelFailed);
                UIManager.Instance.ShowPopupLoseGame();
            }
        });
    }
    
    public void OnClick(Vector3 pos)
    {
        pos = Camera.main.ScreenToWorldPoint(pos);
        DebugCustom.Log($"OnClick {pos}");
    }
    
    public void DoWaitAction(float time, System.Action callback)
    {
        StartCoroutine(IEWaitAction(time, callback));
    }

    IEnumerator IEWaitAction(float time, System.Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
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