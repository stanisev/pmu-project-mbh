using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Leaderboard : MonoBehaviour
{

    DatabaseReference DBreference;

    public GameObject scoreElement;
    public Transform scoreboardContent;

    public GameObject LeaderboardUI;


    public void LeaderboardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }

    public void CloseLeaderboard()
    {
        LeaderboardUI.SetActive(false);
    }
    private IEnumerator LoadScoreboardData()
    {
        

        var DBTask = FirebaseDatabase.DefaultInstance.RootReference
            .Child("data")
            .OrderByChild("_totalRightAnswers")
            .GetValueAsync();

        //var DBTask = FirebaseDatabase.DefaultInstance.GetReference("/")
        //    .Child("data").OrderByChild("_totalRightAnswers").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

           foreach (Transform child in scoreboardContent.transform)
           {
               Destroy(child.gameObject);
           }

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int right = int.Parse(childSnapshot.Child("_totalRightAnswers").Value.ToString());
                int wrong = int.Parse(childSnapshot.Child("_totalWrongAnswers").Value.ToString());
                int trophie = int.Parse(childSnapshot.Child("_currentRank").Value.ToString());

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, right, wrong, trophie);

                Debug.Log(username + " username");
            }

            LeaderboardUI.SetActive(true);
            //Go to scoareboard screen
            //UIManager.instance.ScoreboardScreen();
        }
    }
}
