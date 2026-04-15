using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum ESfx
{
    ButtonSfx = 0,
    RewardedSfx = 1,
    ReceiveCoinSfx = 2,
    WinSfx = 3,
    LoseSfx = 4,
    ArrowMoveSfx = 5,
    FoodFlySfx = 6,
    OrderComplete = 7,
    FoodJumpSfx = 8,
    FoodLandSfx = 9,
    BoosterSfx = 10,
}
[CreateAssetMenu(fileName = "Data Sound Effect", menuName = "Data/Data Sound Effect")]
public class DataSoundEffect : SerializedScriptableObject
{
    public Dictionary<ESfx, AudioClip> dicSfx;

    [Button]
    void GenDic()
    {
        if (dicSfx == null)
            dicSfx = new Dictionary<ESfx, AudioClip>();
        foreach (var eSfx in Helper.GetListEnum<ESfx>())
        {
            if(!dicSfx.ContainsKey(eSfx))
            {
                dicSfx.Add(eSfx, null);
            }
        }
    }
}
