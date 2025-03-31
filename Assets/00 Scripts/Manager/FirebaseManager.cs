using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using Firebase.RemoteConfig;

public class FirebaseManager : Singleton<FirebaseManager>
{
    FirebaseRemoteConfig remoteConfig;
    static Dictionary<string, string> _remoteConfig = new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Firebase is ready to use");
                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener
                  += ConfigUpdateListenerEventHandler;
                FetchDataAsync();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
    #region Analystic
    public static void LogEvent(string eventName)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName);
    }
    public static void LogEventParmeter(string eventName, Dictionary<string, string> parametesString)
    {
        List<Firebase.Analytics.Parameter> parameters = new List<Firebase.Analytics.Parameter>();
        foreach (var item in parametesString)
        {
            parameters.Add(new Firebase.Analytics.Parameter(item.Key, item.Value));
        }
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName, parameters.ToArray());
    }
    #endregion

    #region RemoteConfig
    public static string GetRemoteConfig(string key, string defautValue = "")
    {
        if (_remoteConfig.ContainsKey(key))
        {
            return _remoteConfig[key];
        }
        return defautValue;
    }
    public Task FetchDataAsync()
    {
        Debug.Log("Fetching data...");
        System.Threading.Tasks.Task fetchTask =
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }
    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            Debug.LogError("Retrieval hasn't finished.");
            return;
        }

        remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {
            Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            return;
        }

        // Fetch successful. Parameter values must be activated to use.
        remoteConfig.ActivateAsync()
          .ContinueWithOnMainThread(
            task =>
            {
                Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");
                foreach (var item in remoteConfig.AllValues)
                {
                    if (!_remoteConfig.ContainsKey(item.Key))
                        _remoteConfig.Add(item.Key, item.Value.StringValue);
                    else
                        _remoteConfig[item.Key] = item.Value.StringValue;
                }
                TigerForge.EventManager.EmitEvent(Constant.EVENT_ON_REMOTE_CONFIG_UPDATE);
            });
    }
    // Handle real-time Remote Config events.
    void ConfigUpdateListenerEventHandler(
       object sender, Firebase.RemoteConfig.ConfigUpdateEventArgs args)
    {
        if (args.Error != Firebase.RemoteConfig.RemoteConfigError.None)
        {
            Debug.Log(String.Format("Error occurred while listening: {0}", args.Error));
            return;
        }

        Debug.Log("Updated keys: " + string.Join(", ", args.UpdatedKeys));
        // Activate all fetched values and then display a welcome message.
        FetchDataAsync();
    }

    // Stop the listener.
    void OnDestroy()
    {
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener
          -= ConfigUpdateListenerEventHandler;
    }
    #endregion
}
