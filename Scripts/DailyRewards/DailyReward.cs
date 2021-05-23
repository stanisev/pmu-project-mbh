using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class DailyReward : MonoBehaviour
{
    //UI
    public TMP_Text timeLabel; //only use if your timer uses a label
    public Button timerButton; //used to disable button when needed
  //  public Image _progress;
    //TIME ELEMENTS
    public int hours; //to set the hours
    public int minutes; //to set the minutes
    public int seconds; //to set the seconds
    private bool _timerComplete = false;
    private bool _timerIsReady;
    private TimeSpan _startTime;
    private TimeSpan _endTime;
    private TimeSpan _remainingTime;
    //progress filler
    private float _value = 1f;
    //reward to claim
    public int RewardToEarn;

    public TMP_Text remainingText;

    public Animator animator;
    //startup
    void Start()
    {
        if (PlayerPrefs.GetString("_timer") == "")
        {
            enableButton();
        }
        else
        {
            disableButton();
            StartCoroutine("CheckTime");
        }
    }



    //update the time information with what we got some the internet
    private void updateTime()
    {
        if (PlayerPrefs.GetString("_timer") == "Standby")
        {
            PlayerPrefs.SetString("_timer", TimeManager.instance.getCurrentTimeNow());
            PlayerPrefs.SetInt("_date", TimeManager.instance.getCurrentDateNow());
        }
        else if (PlayerPrefs.GetString("_timer") != "" && PlayerPrefs.GetString("_timer") != "Standby")
        {
            int _old = PlayerPrefs.GetInt("_date");
            int _now = TimeManager.instance.getCurrentDateNow();


            //check if a day as passed
            if (_now > _old)
            {//day as passed
                Debug.Log("Day has passed");
                enableButton();
                return;
            }
            else if (_now == _old)
            {//same day
                Debug.Log("Same Day - configuring now");
                _configTimerSettings();
                return;
            }
            else
            {
                Debug.Log("error with date");
                return;
            }
        }
        Debug.Log("Day had passed - configuring now");
        _configTimerSettings();
    }

    //setting up and configureing the values
    //update the time information with what we got some the internet
    private void _configTimerSettings()
    {
        _startTime = TimeSpan.Parse(PlayerPrefs.GetString("_timer"));
        _endTime = TimeSpan.Parse(hours + ":" + minutes + ":" + seconds);
        //Debug.Log("START TIME" + _startTime);
        //Debug.Log("END TIME" + _endTime);
        TimeSpan temp = TimeSpan.Parse(TimeManager.instance.getCurrentTimeNow());
        TimeSpan diff = temp.Subtract(_startTime);
        _remainingTime = _endTime.Subtract(diff);
        remainingText.text = _remainingTime.ToString();
        //Debug.Log("REMAIN" + _remainingTime);
        //start timmer where we left off
       // setProgressWhereWeLeftOff();

        if (diff >= _endTime)
        {
            _timerComplete = true;
            enableButton();
        }
        else
        {
            _timerComplete = false;
            disableButton();
            _timerIsReady = true;
            validateTime();
        }
    }

    //initializing the value of the timer
    private void setProgressWhereWeLeftOff()
    {
        float ah = 1f / (float)_endTime.TotalSeconds;
        float bh = 1f / (float)_remainingTime.TotalSeconds;
        _value = ah / bh;
        //_progress.fillAmount = _value;
    }



    //enable button function
    private void enableButton()
    {
        timerButton.interactable = true;
        timeLabel.text = "+ 2000";
        animator.enabled = true;
    }



    //disable button function
    private void disableButton()
    {
        timerButton.interactable = false;
        timeLabel.text = "X X X X X";
        animator.enabled = false;
    }


    //use to check the current time before completely any task. use this to validate
    private IEnumerator CheckTime()
    {
        disableButton();
        timeLabel.text = "X-X-X-X-X";
        yield return StartCoroutine(
            TimeManager.instance.getTime()
        );
        updateTime();

    }


    //trggered on button click
    public void rewardClicked()
    {
        AudioPicker.instance.buttonClicked.Play();
        claimReward(RewardToEarn);
        PlayerPrefs.SetString("_timer", "Standby");
        StartCoroutine("CheckTime");
    }



    //update method to make the progress tick
 //void Update()
 //{
 //    if (_timerIsReady)
 //    {
 //        if (!_timerComplete && PlayerPrefs.GetString("_timer") != "")
 //        {
 //            _value -= Time.deltaTime * 1f / (float)_endTime.TotalSeconds;
 //           // _progress.fillAmount = _value;
 //
 //            //this is called once only
 //            if (_value <= 0 && !_timerComplete)
 //            {
 //                //when the timer hits 0, let do a quick validation to make sure no speed hacks.
 //                validateTime();
 //                _timerComplete = true;
 //            }
 //        }
 //    }
 //}



    //validator
    private void validateTime()
    {
        StartCoroutine("CheckTime");
    }


    private void claimReward(int x)
    {
        DataUpdater.instance.SetCoins(x);
    }

}