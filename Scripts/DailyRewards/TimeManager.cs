using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Singleton

    public static TimeManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DailyRewards found!!!");
        }
        instance = this;
    }
    #endregion

    private string _url = "http://leatonm.net/wp-content/uploads/2017/candlepin/getdate.php";

    private string _timeData;
    private string _currentTime;
    private string _currentDate;

    public GameObject dailyRewatdCanvas;


    //time fether coroutine
    public IEnumerator getTime()
    {
        WWW www = new WWW(_url);

        yield return www;

        _timeData = www.text;
        string[] words = _timeData.Split('/');
        _currentDate = words[0];
        _currentTime = words[1];

    // string[] number = _currentTime.Split(':');
    // int y = int.Parse(number[0]) + 5;
    // _currentTime = y.ToString() + ":" + number[1] + ":" + number[2];
    //
       
    }


    //get the current time at startup
    void Start()
    {
        StartCoroutine("getTime");
    }

    //get the current date - also converting from string to int.
    //where 12-4-2017 is 1242017
    public int getCurrentDateNow()
    {
        string[] words = _currentDate.Split('-');
        int x = int.Parse(words[0] + words[1] + words[2]);
        return x;
    }


    //get the current Time
    public string getCurrentTimeNow()
    {
        return _currentTime;
    }


    public void OpenDailyReard()
    {
        AudioPicker.instance.buttonClicked.Play();
        dailyRewatdCanvas.SetActive(true);
    }

    public void CloseDailyReard()
    {
        AudioPicker.instance.buttonClicked.Play();
        dailyRewatdCanvas.SetActive(false);
    }

}
