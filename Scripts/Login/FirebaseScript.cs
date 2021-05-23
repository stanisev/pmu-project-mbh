using UnityEngine;
using Firebase;
using Firebase.Database;

public class FirebaseScript : MonoBehaviour
{

    DatabaseReference reference;

    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                // app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        // FirebaseDatabase.DefaultInstance.GetReferenceFromUrl("https://pmu-b-646bf-default-rtdb.europe-west1.firebasedatabase.app/");

        //reference = FirebaseDatabase.DefaultInstance.RootReference;
        TypeOfUser.instance.SET_DB(FirebaseDatabase.DefaultInstance.RootReference);

    }


}