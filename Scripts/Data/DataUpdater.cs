using Firebase.Database;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataUpdater : MonoBehaviour
{
    #region Singleton Data

    public static DataUpdater instance;
    string baseToken;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DataUpdater found!!!");
        }
        instance = this;
        baseToken = TypeOfUser.instance.GET_USER_TYPE();
    }

    #endregion

    [SerializeField] public Data data;

    public TextMeshProUGUI Coins;
    public TextMeshProUGUI Energy;
    public TMP_Text Spins;

    public TMP_Text totalCorrectAnswers;
    public TMP_Text totalWrongAnswers;

    public TMP_Text rank;

    public TMP_Text usernameText;

    public void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        // data = ScriptableObject.CreateInstance<Data>();
        data = Resources.Load<Data>("Datas/Data");
        RetriveFromBase();
        //   data = LoadData();
        //   UpdateAmount(data);
    }
    public void Update()
    {
        UpdateAmount(data);
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saves/" + "DataSave" + ".stanisev", json);
    }

    public Data LoadData()
    {
        Data data = null;
        if (File.Exists(Application.persistentDataPath + "/saves/" + "DataSave" + ".stanisev"))
        {
            data = ScriptableObject.CreateInstance<Data>();
            string json = File.ReadAllText(Application.persistentDataPath + "/saves/" + "DataSave" + ".stanisev");
            JsonUtility.FromJsonOverwrite(json, data);

        }
        else {  data = Resources.Load<Data>("Datas/Data"); }
        return data;
    }
    public void UpdateAmount(Data _data)
    {
        Coins.text = _data._totalCoins.ToString("f0");
        if(data._totalEnergies <= 0) { data._totalEnergies = 0; }
        Energy.text = _data._totalEnergies.ToString();
        Spins.text = _data._totalSpins.ToString();

        totalCorrectAnswers.text = _data._totalRightAnswers.ToString();
        totalWrongAnswers.text = _data._totalWrongAnswers.ToString();

        rank.text = _data._currentRank.ToString();

        usernameText.text = _data.username;

    }

    public void SetCoins(float _coins)
    {
        data._totalCoins += _coins;
    }
    
    public void SetEnergy(int _energy)
    {
        data._totalEnergies += _energy;
    }
    public float GetCoins()
    {
        return data._totalCoins;
    }

    public float GetEnergy()
    {
        return data._totalEnergies;
    }

    public void SetWrongAnswer(int _answer)
    {
        data._totalWrongAnswers += _answer;
    }
    public void SetCorrectAnswer(int _answer)
    {
        data._totalRightAnswers += _answer;
    }

    public void SetRank(int _rank)
    {
        data._currentRank += _rank;
    }

    public int GetRank()
    {
        return data._currentRank;
    }

    public int GetWrongAnswer()
    {
        return data._totalWrongAnswers;
    }
    public int GetCorrectAnswer()
    {
        return data._totalRightAnswers;
    }

    public int GetSpins()
    {
        return data._totalSpins;
    }

    public void SetSpins(int _spins)
    {
        data._totalSpins += _spins;
    }

    public void SetAuthId(string _token)
    {
        baseToken = _token;
    }

    public void SetUserName(string _username)
    {
        data.username = _username;
    }

    public string GetUserName()
    {
        return data.username;
    }

    public void ToBase()
    {
        RestClient.Put<Data>("https://pmu-b-646bf-default-rtdb.europe-west1.firebasedatabase.app/" + "data/" + baseToken  + ".json", data);
    }
    public void RetriveFromBase()
    {

        FirebaseDatabase.DefaultInstance.GetReference("data/" + baseToken)
              .GetValueAsync().ContinueWith((task =>
              {
                  if (task.IsCompleted)
                  {
                      DataSnapshot shapshot = task.Result;
                      string playerData = shapshot.GetRawJsonValue();
                      JsonUtility.FromJsonOverwrite(playerData, data);
                  }
              }));

       

  //  FirebaseDatabase.DefaultInstance.RootReference
  //      .Child("data").Child(baseToken).
  //        GetValueAsync().ContinueWith((task =>
  //        {
  //            if (task.IsCompleted)
  //            {
  //                DataSnapshot shapshot = task.Result;
  //                string playerData = shapshot.GetRawJsonValue();
  //                JsonUtility.FromJsonOverwrite(playerData, data);
  //            }
  //        }));
    }

    public void OnApplicationQuit()
    {
        ToBase();
    }


    //public void RetriveFromBase()
    //{
    //    string url = "https://pmu-b-646bf-default-rtdb.europe-west1.firebasedatabase.app/" + baseToken + "/" + "data";
    //    RestClient.Get<Data>(url).Then(response => {
    //        var responseJson = response.ToString();
    //
    //        var data_ = fsJsonPa
    //
    //    });
    //
    //
    //
    //}
    //
}
