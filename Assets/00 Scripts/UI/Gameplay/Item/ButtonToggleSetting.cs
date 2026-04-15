using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggleSetting : MonoBehaviour
{
    public EGameSetting settingType;
    public Button btn;
    protected void Start()
    {
        btn.onClick.AddListener(OnCick);
        TigerForge.EventManager.StartListening(Constant.EVENT_ON_GAME_SETTING_CHANGE, InitUI);
        InitUI();
    }
    void InitUI()
    {
        bool setting = GameSettingController.Instance.GetSetting(settingType);
        btn.image.color = setting ? Color.white : Color.gray;
    }
    public void OnCick()
    {
        AudioManager.Instance.PlaySfx(ESfx.ButtonSfx);
        GameSettingController.Instance.ToggleSetting(settingType);
    }
}
