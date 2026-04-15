using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T Instance;
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Debug.Log($"Instance: {nameof(T)} already exists, destroying duplicate!");
            Debug.Log($"Destroy {gameObject.name}");
            Destroy(gameObject);
        }
    }
}

public abstract class SingletonController<T, D> where T : SingletonController<T, D>, new() where D : ControllerCachedData, new()
{
    public static T Instance = new();
    protected D cachedData;

    protected abstract string KeyData();
    protected abstract string KeyEvent();
    bool firstTime = true;
    bool firstTick = true;
    private bool waitSaveData = false;
    public void Init()
    {
        string json = PlayerPrefs.GetString(KeyData());
        if (string.IsNullOrEmpty(json))
        {
            cachedData = new D();
            cachedData.OnNewData();
            OnNewData();
            SaveData();
        }
        else
        {
            cachedData = Newtonsoft.Json.JsonConvert.DeserializeObject<D>(json);
        }
        cachedData.FirstTimeInit();
        if(firstTime)
            FirstTimeInit();
        OnInitSuccess();
    }

    protected void SaveData()
    {
        waitSaveData = false;
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(cachedData);
        PlayerPrefs.SetString(KeyData(), json);
        PlayerPrefs.Save();
    }

    protected virtual void OnNewData()
    {
        
    }
    
    protected virtual void FirstTimeInit()
    {
        firstTime = false;
        TigerForge.EventManager.StartListening(Constant.EVENT_TIMER_TICK, OnTick);
        TigerForge.EventManager.StartListening(Constant.EVENT_TIMER_UPDATE, OnUpdate);
        TigerForge.EventManager.StartListening(Constant.EVENT_TIMER_NEW_DAY, OnNextDay);
        TigerForge.EventManager.StartListening(Constant.EVENT_TIMER_NEW_WEEK, OnNextWeek);
        TigerForge.EventManager.StartListening(Constant.EVENT_TIMER_NEW_MONTH, OnNextMonth);
    }

    protected virtual void OnInitSuccess()
    {
        
    }

    protected virtual void OnNextDay()
    {
        
    }

    protected virtual void OnNextWeek()
    {
        
    }

    protected virtual void OnNextMonth()
    {
        
    }

    protected virtual void OnTick()
    {
        if(firstTick)
            OnFirstTick();
    }

    protected virtual void OnFirstTick()
    {
        firstTick = false;
    }

    void OnUpdate()
    {
        if(waitSaveData)
            SaveData();
    }
    protected virtual void OnValueChange()
    {
        waitSaveData = true;
        TigerForge.EventManager.EmitEvent(KeyEvent());
    }
    public void ClearData()
    {
        PlayerPrefs.DeleteKey(KeyData());
    }
}

public abstract class ControllerCachedData
{
    public abstract void OnNewData();
    public abstract void FirstTimeInit();
}