using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;

public class FirebaseLevel : MonoBehaviour
{
    void Start()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
    }

    private void OnDestroy()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd);
    }
}
