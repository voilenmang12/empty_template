using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePlayButton : HomeFeatureButton
{
    protected override void Start()
    {
        base.Start();
        txtVisual.text = $"LEVEL\n{(AchievementController.Instance.GetAchievementProgress(EAchievementType.LevelCompleted) + 1).ToString("D2")}";
    }

    public override void OnClick()
    {
        GameManager.Instance.PlayGame(EGameType.Campaign);
    }

    protected override void CheckActive()
    {
    }

    protected override void CheckNoti()
    {
    }
}
