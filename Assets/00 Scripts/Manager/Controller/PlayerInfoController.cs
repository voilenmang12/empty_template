using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoCachedData : ControllerCachedData
{
    public Dictionary<int, int> dicPlayedLevel = new Dictionary<int, int>();
    
    public override void OnNewData()
    {
    }

    public override void FirstTimeInit()
    {
    }

    public void SetPlay(int level)
    {
        if(!dicPlayedLevel.ContainsKey(level))
            dicPlayedLevel.Add(level, 0);
        dicPlayedLevel[level]++;
    }

    public int GetPlayLevel(int level)
    {
        if (!dicPlayedLevel.ContainsKey(level))
            return 0;
        return dicPlayedLevel[level];
    }
}
public class PlayerInfoController : SingletonController<PlayerInfoController, PlayerInfoCachedData>
{
    protected override string KeyData()
    {
        return "player_info";
    }

    protected override string KeyEvent()
    {
        return Constant.EVENT_ON_PLAYER_INFO_CHANGE;
    }

    public void SetPlayed()
    {
        cachedData.SetPlay(AchievementController.Instance.GetAchievementProgress(EAchievementType.LevelCompleted) + 1);
        OnValueChange();
    }

    public int GetLevelPlayed()
    {
        return cachedData.GetPlayLevel(AchievementController.Instance.GetAchievementProgress(EAchievementType.LevelCompleted) + 1);
    }
}
