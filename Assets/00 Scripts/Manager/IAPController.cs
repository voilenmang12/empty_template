using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class IAPCachedData : ControllerCachedData
{
    public Dictionary<string, int> dicPackBought = new Dictionary<string, int>();

    public override void OnNewData()
    {
        
    }
    public override void FirstTimeInit()
    {
        
    }
    public void SetPackBought(EIAPPackType packType)
    {
        if(!dicPackBought.ContainsKey(packType.ToString()))
            dicPackBought.Add(packType.ToString(), 0);
        dicPackBought[packType.ToString()]++;
    }
    public int GetPackBought(EIAPPackType packType)
    {
        if (!dicPackBought.ContainsKey(packType.ToString()))
            return 0;
        return dicPackBought[packType.ToString()];
    }
}
public class IAPController : SingletonController<IAPController, IAPCachedData>
{
    protected override string KeyData()
    {
        return "data_iap";
    }

    protected override string KeyEvent()
    {
        return Constant.EVENT_ON_IAP_CHANGE;
    }

    public int GetPackBought(EIAPPackType packType)
    {
        return cachedData.GetPackBought(packType);
    }

    public void OnPurchaseSuccess(string packId, string transactionID)
    {
        Debug.Log($"Purchase Success: {packId}");
        EIAPPackType packType = DataSystem.Instance.dataIAP.dicId[packId];
        DataSystem.Instance.dataIAP.dicConfigs[packType].GetReward().ReceiveAndShow(EResourceFrom.IAP);
        cachedData.SetPackBought(packType);
        OnValueChange();
    }

    public void PurchasePack(EIAPPackType packType)
    {
        IAPManager.Instance.PurchaseIAP(DataSystem.Instance.dataIAP.dicConfigs[packType].packID);
    }
}