using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource musicSource;
    public float maxMusicVolume = 0.6f;
    public SfxAudioSource sfxSourcePrefabs;
    List<int> lstPlayingSource;
    bool initilized;
    public void GameInit()
    {
        lstPlayingSource = new List<int>();
        TigerForge.EventManager.StartListening(Constant.EVENT_ON_GAME_SETTING_CHANGE, OnGameSettingChange);
        OnGameSettingChange();
        initilized = true;
        musicSource.Play();
    }

    public void OnGameSettingChange()
    {
        musicSource.volume = GameSettingController.Instance.GetSetting(EGameSetting.Music) ? maxMusicVolume : 0;
    }

    private void Update()
    {
        if (!initilized)
            return;
        lstPlayingSource.Clear();
    }

    public SfxAudioSource PlaySfx(AudioClip sfx)
    {
        if (GameManager.Instance.GameState != EGameState.Home && GameManager.Instance.GameState != EGameState.Gameplay)
            return null;
        if (!GameSettingController.Instance.GetSetting(EGameSetting.Sound) || lstPlayingSource.Contains(sfx.GetInstanceID()))
            return null;
        //DebugCustom.Log("Play Sound", sfx);
        lstPlayingSource.Add(sfx.GetInstanceID());
        SfxAudioSource source = ObjectPooler.Spawn(sfxSourcePrefabs, transform.position);
        source.PlaySfx(sfx);
        return source;
    }

    public SfxAudioSource PlaySfx(ESfx sfx)
    {
        return PlaySfx(DataSystem.Instance.dataSoundEffect.dicSfx[sfx]);
    }
}