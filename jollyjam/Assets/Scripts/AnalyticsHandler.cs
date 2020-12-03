using System.Collections.Generic;
using UnityEngine.Analytics;

public static class AnalyticsHandler
{
    public static void SendAnalyticsEvent(string eventName, Dictionary<string, object> dictionary)
    {
        //       UnityEngine.Analytics.Analytics.CustomEvent(eventName, dictionary);
        Analytics.CustomEvent(eventName, dictionary);
        //Debug.Log("Event send"+ eventName);
    }

    private static void ExampleToSend(int levelCount)
    {
        AnalyticsHandler.SendAnalyticsEvent("LevelFinish", new Dictionary<string, object>
        {
            {"Level_"+ levelCount, 12f},
        });
    }
}
