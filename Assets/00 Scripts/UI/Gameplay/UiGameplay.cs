using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UiGameplay : MonoBehaviour
{
    public TextMeshProUGUI txtLevel;

    private void Start()
    {
        UIManager.Instance.uIGameplay = this;
    }

    public void Initialize()
    {
        TigerForge.EventManager.StartListening(Constant.EVENT_TIMER_TICK, OnTick);
        InitLevel();
        TigerForge.EventManager.StartListening(Constant.EVENT_LEVEL_INITED, InitLevel);
        OnTick();
    }

    void InitLevel()
    {
        txtLevel.text = $"Level {GameplayManager.Instance.LevelId}";
    }

    void OnTick()
    {
    }

    public void OnClickPauseGame()
    {
        if (GameplayManager.Instance.State == EGamePlayState.Running)
            UIManager.Instance.ShowPopupPauseGame();
    }

    public void OnClickRestartGame()
    {
        GameManager.Instance.PlayGame(GameManager.Instance.GameType);
    }
}