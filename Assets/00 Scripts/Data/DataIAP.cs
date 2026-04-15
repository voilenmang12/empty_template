using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
[CreateAssetMenu(fileName = "Data IAP", menuName = "Data/Data IAP")]
public class DataIAP : SerializedScriptableObject
{
    public Dictionary<EIAPPackType, IAPConfig> dicConfigs;
    public Dictionary<string, EIAPPackType> dicId;
    public Dictionary<EIAPPackType, Sprite> dicPackIcon = new Dictionary<EIAPPackType, Sprite>();
    
    
#if UNITY_EDITOR
    [Button]
    void CheckDic()
    {
        foreach (var item in dicConfigs)
        {
            if(!dicPackIcon.ContainsKey(item.Key))
                dicPackIcon.Add(item.Key, null);
        }
    }
    [Button()]
    void LoadData()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vSkEDBkwkDH2nbD6aUHCM1nTLq8-L-DBdggx4Qi06XxJCurky_I7yVUS4JtTxqhnBqDptstW15Kbeqg/pub?gid=0&single=true&output=csv";
        System.Action<string> actionComplete = new System.Action<string>((string str) =>
        {
            dicId = new Dictionary<string, EIAPPackType>();
            dicConfigs = new Dictionary<EIAPPackType, IAPConfig>();
            var data = CSVReader.ReadCSV(str);
            for (int i = 1; i < data.Count; i++)
            {
                var _data = data[i];
                if (!string.IsNullOrEmpty(_data[0]))
                {
                    if (Helper.TryToEnum(_data[0], out EIAPPackType packType))
                    {
                        IAPConfig config = new IAPConfig();
                        config.packType = packType;
                        config.packID = _data[1];
                        config.packName = _data[2];
                        config.maxBuy = Helper.ParseInt(_data[3]);
                        config.price = Helper.ParseFloat(_data[4]);
                        config.lstReward = new List<string>();
                        for (int j = 5; j < _data.Length; j++)
                        {
                            if (!string.IsNullOrEmpty(_data[j]))
                                if (GameResource.GetResource(_data[j]) != null)
                                    config.lstReward.Add(_data[j]);
                        }
                        dicConfigs.Add(packType, config);
                        dicId.Add(config.packID, packType);
                    }
                }
            }

            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Helper.IELoadData(url, actionComplete, name));
    }
#endif
}
public class IAPConfig
{
    public EIAPPackType packType;
    public string packID;
    public string packName;
    public int maxBuy;
    public float price;
    public List<string> lstReward;
    public bool CanShow()
    {
        return maxBuy == 0 || maxBuy > IAPController.Instance.GetPackBought(packType);
    }
    public PackageResource GetReward()
    {
        PackageResource pack = new PackageResource();
        foreach (var item in lstReward)
        {
            pack.AddResource(GameResource.GetResource(item));
        }
        return pack;
    }
}
