using System.Collections;
using System.Collections.Generic;
using Firebase;
using UnityEngine;

public class FirebaseSetup : MonoBehaviour
{
    private Firebase.FirebaseApp app;

    [SerializeField] private TextAsset asset;
    
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;
                //Firebase.AppOptions options = AppOptions.LoadFromJsonConfig(asset.text);
               // app = Firebase.FirebaseApp.Create(options);

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
}
