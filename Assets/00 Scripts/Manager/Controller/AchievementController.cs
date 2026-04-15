using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AchievementCachedData : ControllerCachedData
{
    public Dictionary<string, int> dicAchivementProgress = new Dictionary<string, int>();

    public override void OnNewData()
    {
    }

    public override void FirstTimeInit()
    {
    }

    public int GetAchievementProgress(EAchievementType achievementType)
    {
        if (!dicAchivementProgress.ContainsKey(achievementType.ToString()))
            return 0;
        return dicAchivementProgress[achievementType.ToString()];
    }
    public void UpdateAchievementProgress(EAchievementType achievementType, int val = 1, bool replace = false)
    {
        if(!dicAchivementProgress.ContainsKey(achievementType.ToString()))
            dicAchivementProgress.Add(achievementType.ToString(), 0);
        if (!replace)
            dicAchivementProgress[achievementType.ToString()] += val;
        else
            dicAchivementProgress[achievementType.ToString()] = val;
    }
}
public class AchievementController : SingletonController<AchievementController, AchievementCachedData>
{
    protected override string KeyData()
    {
        return "achievement";
    }

    protected override string KeyEvent()
    {
        return Constant.EVENT_ON_ACHIEVEMENT_CHANGE;
    }

    protected override void OnNextDay()
    {
        base.OnNextDay();
        UpdateAchievementProgress(EAchievementType.Login);
    }
    
    public int GetAchievementProgress(EAchievementType achievementType)
    {
        return cachedData.GetAchievementProgress(achievementType);
    }

    public void UpdateAchievementProgress(EAchievementType achievementType, int val = 1, bool replace = false)
    {
        cachedData.UpdateAchievementProgress(achievementType, val, replace);
        // DailyQuestController.Instance.UpdateQuestProgress(achievementType, val, replace);
        OnValueChange();
    }
}