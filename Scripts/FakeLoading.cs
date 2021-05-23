using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class FakeLoading : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI showProgress;

    private float zeroTime = 0f;

    private string userName;

    public GameObject UserNameUI;
    private int canLoad = 0;

    private string EMPTY = "";
    private string CACHE_USERNAME = "usernameCCZYXx";
    void Start()
    {

        userName = null;
        userName = PlayerPrefs.GetString(CACHE_USERNAME);

        canLoad = 0;
        canLoad = PlayerPrefs.GetInt("load1x");

        TypeOfUser.instance.SET_USERNAME(userName);

        Debug.Log(userName);

        if (userName == null || EMPTY.Equals(userName))
        {
            UserNameUI.SetActive(true);

        } else
        {
            StartCoroutine(randomSeconds(2f));
        }

        

    }

    public IEnumerator randomSeconds(float interval)
    {
       
        yield return new WaitForSeconds(interval);
        SceneManager.LoadSceneAsync(2);

    }

    public void Update()
    {
        if (canLoad==1)
        {
            zeroTime += Time.deltaTime * 1.5f;
            slider.value = zeroTime / 4.5f;
            showProgress.text = (zeroTime * 20).ToString("f0") + "%";
        }
    }

    public void ReadStringInput(string input)
    {
        this.userName = input;

        PlayerPrefs.SetString(CACHE_USERNAME, userName);
        TypeOfUser.instance.SET_USERNAME(userName);

        UserNameUI.SetActive(false);

        canLoad = 1;
        PlayerPrefs.SetInt("load1x", canLoad);

        StartCoroutine(randomSeconds(2f));
    }
}
