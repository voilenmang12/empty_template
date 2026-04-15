using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerResourceCachedData : ControllerCachedData
{
    public Dictionary<string, int> dicCommonResource = new Dictionary<string, int>();
    public override void OnNewData()
    {
    }

    public override void FirstTimeInit()
    {
    }

    public int GetCommonResource(ECommonResource resourceType)
    {
        if (dicCommonResource.ContainsKey(resourceType.ToString()))
            return dicCommonResource[resourceType.ToString()];
        return 0;
    }

    public void AddCommonResource(ECommonResource resourceType, int value)
    {
        if (!dicCommonResource.ContainsKey(resourceType.ToString()))
            dicCommonResource.Add(resourceType.ToString(), 0);
        dicCommonResource[resourceType.ToString()] += value;
    }
    
    public bool CheckListResource(List<GameResource> lstResources)
    {
        foreach (GameResource resource in lstResources)
        {
            if (resource is CommonResource commonResource)
            {
                if (GetCommonResource(commonResource.resourceType) + commonResource.resourceValue < 0)
                    return false;
            }
        }
        return true;
    }

    public void AddResource(List<GameResource> lstResources)
    {
        foreach (GameResource resource in lstResources)
        {
            if (resource is CommonResource commonResource)
            {
                AddCommonResource(commonResource.resourceType, commonResource.resourceValue);
            }
        }
    }
}
public class PlayerResource : SingletonController<PlayerResource, PlayerResourceCachedData>
{
    protected override string KeyData()
    {
        return "player_resource";
    }

    protected override string KeyEvent()
    {
        return Constant.ON_PLAYER_RESOURCE_UPDATE;
    }

    public bool CheckListResource(List<GameResource> lstResources)
    {
        return cachedData.CheckListResource(lstResources);
    }
    public void AddResource(List<GameResource> lstResource, EResourceFrom resourceFrom, Action actionSuccess = null, Action actionError = null)
    {
        if (!CheckListResource(lstResource))
        {
            UIManager.Instance.ShowDialog("Not Enough Resources");
            actionError?.Invoke();
            return;
        }
        cachedData.AddResource(lstResource);
        OnValueChange();
        actionSuccess?.Invoke();
    }

    public int GetCommonResource(ECommonResource resourceType)
    {
        return cachedData.GetCommonResource(resourceType);
    }
}