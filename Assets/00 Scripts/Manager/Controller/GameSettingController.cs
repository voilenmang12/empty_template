using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingCachedData : ControllerCachedData
{
    public Dictionary<string, bool> dicGameSetting = new Dictionary<string, bool>();

    public override void OnNewData()
    {
    }

    public override void FirstTimeInit()
    {
    }

    public void ToggleSetting(EGameSetting setting)
    {
        if (!dicGameSetting.ContainsKey(setting.ToString()))
            dicGameSetting.Add(setting.ToString(), true);
        dicGameSetting[setting.ToString()] = !dicGameSetting[setting.ToString()];
    }

    public bool GetSetting(EGameSetting setting)
    {
        if (!dicGameSetting.ContainsKey(setting.ToString()))
            return true;
        return dicGameSetting[setting.ToString()];
    }
}

public class GameSettingController : SingletonController<GameSettingController, GameSettingCachedData>
{
    protected override string KeyData()
    {
        return "game_setting";
    }

    protected override string KeyEvent()
    {
        return Constant.EVENT_ON_GAME_SETTING_CHANGE;
    }

    public bool GetSetting(EGameSetting setting)
    {
        return cachedData.GetSetting(setting);
    }

    public void ToggleSetting(EGameSetting setting)
    {
        cachedData.ToggleSetting(setting);
        OnValueChange();
    }
}