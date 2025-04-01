using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public interface IIAPController : IController<IAPControllerLocal>
{
    public int GetPackBought(EIAPPackType packType);
    public void PurchasePack(EIAPPackType packType);
    public void OnPurchaseSuccess(string packId, string transactionID);
    public void FetchHistory();
    public IAPHistoryResponse GetHistory();
    public bool NotiUI();
    public bool IsNotiMessage(string id);
    public void OnShowUI();
}
public class IAPCachedData : IControllerCachedData
{
    public List<string> lstPackHandled = new List<string>();
    public Dictionary<string, int> dicPackBought = new Dictionary<string, int>();
    public void InitFirsTime()
    {
        List<EIAPPackType> lstType = Helper.GetListEnum<EIAPPackType>();
        foreach (var item in lstType)
        {
            if (!dicPackBought.ContainsKey(item.ToString()))
                dicPackBought.Add(item.ToString(), 0);
        }
    }
    public void OnNewData()
    {
    }
    public int GetPackBought(EIAPPackType packType)
    {
        return dicPackBought[packType.ToString()];
    }
    public void SetPackBought(EIAPPackType packType, string transactionID)
    {
        dicPackBought[packType.ToString()]++;
        SetPackBought(transactionID);
    }
    public void SetPackBought(string transactionID)
    {
        lstPackHandled.Add(transactionID);
    }
    public void SetPackBought(List<string> lstPackHandled)
    {
        this.lstPackHandled = lstPackHandled;
    }
    public bool GetPackHandled(string transactionID)
    {
        return lstPackHandled.Contains(transactionID);
    }
}
public class IAPControllerLocal :
#if LOCAL_BUILD
    BaseLocalController<IAPCachedData>
#else
    CommonServerController<IAPCachedData>
#endif
    , IIAPController
{
    Dictionary<string, string> dicInvoiceLink => DataSystem.Instance.dataIAP.dicInvoice;
    IAPHistoryResponse history;
    List<string> lstNotiMessage = new List<string>();
    int currentPage = 1;
    public override string KeyData()
    {
        return "data_iap";
    }

    public override string KeyEvent()
    {
        return Constant.EVENT_ON_IAP_CHANGE;
    }
    public override IEnumerator IEFetchConfigs()
    {
        IAPManager.Instance.InitializePurchasing();
        yield break;

        DataSystem.Instance.dataIAP.ParseConfig(GameManager.Instance.GetConfig("iap_config"));
        yield return null;
    }
    protected override void OnFirstTick()
    {
        base.OnFirstTick();
        return;
        FetchHistory();
    }
    protected override void FirstTimeInit()
    {
        base.FirstTimeInit();
        return;
        UniRx.Observable.Interval(System.TimeSpan.FromSeconds(30)).Subscribe(_ => FetchHistory()).AddTo(GameManager.Instance.gameObject);
    }
    public int GetPackBought(EIAPPackType packType)
    {
        return cachedData.GetPackBought(packType);
    }

    public void OnPurchaseSuccess(string packID, string transactionID)
    {
        EIAPPackType packType = DataSystem.Instance.dataIAP.dicId[packID];
        DataSystem.Instance.dataIAP.dicConfigs[packType].GetReward().ReceiveAndShow(EResourceFrom.IAP);
        lstNotiMessage.Add(transactionID);
        cachedData.SetPackBought(packType, transactionID);
        OnValueChange();
    }

    public void PurchasePack(EIAPPackType packType)
    {
#if UNITY_WEBGL
        TelegramManager.OpenInvoice(dicInvoiceLink[DataSystem.Instance.dataIAP.dicConfigs[packType].packID]);
#endif
        IAPManager.Instance.PurchaseIAP(DataSystem.Instance.dataIAP.dicConfigs[packType].packID);
    }
    public void SendInvoiceLinkToDev(EIAPPackType packType)
    {
        HTTPManager.Instance.SendInvoiceLinks(new CreateInvoiceLinkMessage
        {
            createInvoiceLinks = new List<InvoiceLinkMessage>()
            {
                DataSystem.Instance.dataIAP.dicConfigs[packType].GetInvoiceMessage(),
            }
        });
    }

    public void FetchHistory()
    {
#if LOCAL_BUILD
        return;
#endif
        history = null;
        OnValueChange();
        HTTPManager.Instance.GetIAPHistory(new GetIAPHistoryRequest
        {
            paging = currentPage,
        }, s =>
        {
            history = s;
            bool pageHandled = false;
            if (history.data.Count == 0)
                pageHandled = true;
            else
                foreach (var item in history.data)
                {
                    if (!cachedData.GetPackHandled(item.id))
                    {
                        if(item.ValidPack())
                            OnPurchaseSuccess(item.shopId, item.id);
                        else
                            cachedData.SetPackBought(item.id);
                    }
                    else
                    {
                        pageHandled = true;
                    }
                }
            if (!pageHandled)
            {
                currentPage++;
                FetchHistory();
            }
            else
            {
                currentPage = 1;
                if (history.page.currentPage != 1)
                    FetchHistory();
                else
                    cachedData.SetPackBought(history.data.Select(t => t.id).ToList());
            }

            OnValueChange();
        });
    }
    public IAPHistoryResponse GetHistory()
    {
        return history;
    }

    public bool NotiUI()
    {
        return lstNotiMessage.Count > 0;
    }

    public void OnShowUI()
    {
        lstNotiMessage.Clear();
    }

    public bool IsNotiMessage(string id)
    {
        return lstNotiMessage.Contains(id);
    }
}
