using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    public GameObject SpinObject;
    public Image RewardImage;
    public Sprite[] rewardImages;

    private int randomValue;
    private float timeInterval;
    private bool coroutineAllowed;
    private int finalAngle;

    public Slider slider;

    [SerializeField]
    private TextMeshProUGUI winText;

    public GameObject closeBtn;

    private void Start()
    {
        coroutineAllowed = true;
        closeBtn.SetActive(true);
        slider.maxValue = 20;
        slider.value = DataUpdater.instance.GetSpins();
    }
    private void Update()
    {
        slider.value = DataUpdater.instance.GetSpins();
    }
    private IEnumerator Spin()
    {
        AudioPicker.instance.spinnerSound.Play();

        coroutineAllowed = false;
        closeBtn.SetActive(false);

        randomValue = Random.Range(20, 30);
        timeInterval = 0.1f;

        for (int i = 0; i < randomValue; i++)
        {
            transform.Rotate(0, 0, 22.5f);
            if (i > Mathf.RoundToInt(randomValue * 0.5f)) { timeInterval = 0.2f; }
            if (i > Mathf.RoundToInt(randomValue * 0.85f)) { timeInterval = 0.4f; }
            yield return new WaitForSeconds(timeInterval);
        }

        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0) { transform.Rotate(0, 0, 22.5f); }

        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);

        switch (finalAngle)
        {
            case 0:
                winText.text = "+300";
                RewardImage.sprite = rewardImages[0];
                DataUpdater.instance.SetCoins(300);
                break;
            case 45:
                winText.text = "+500";
                RewardImage.sprite = rewardImages[0];
                DataUpdater.instance.SetCoins(500);
                break;
            case 90:
                winText.text = "+1";
                RewardImage.sprite = rewardImages[1];
                DataUpdater.instance.SetEnergy(1);
                break;
            case 135:
                winText.text = "+2";
                RewardImage.sprite = rewardImages[1];
                DataUpdater.instance.SetEnergy(2);
                break;
            case 180:
                winText.text = "+1";
                RewardImage.sprite = rewardImages[2];
                DataUpdater.instance.SetSpins(1);
                break;
            case 225:
                winText.text = "+500";
                RewardImage.sprite = rewardImages[0];
                DataUpdater.instance.SetCoins(300);
                break;
            case 270:
                winText.text = "+5";
                RewardImage.sprite = rewardImages[1];
                DataUpdater.instance.SetEnergy(5);
                break;
            case 315:
                winText.text = "+5";
                RewardImage.sprite = rewardImages[2];
                DataUpdater.instance.SetSpins(5);
                break;

        }

        coroutineAllowed = true;
        closeBtn.SetActive(true);
    }

    public void SpinWheel()
    {

        if (coroutineAllowed)
        {
            if (DataUpdater.instance.GetSpins() > 0)
            {
                StartCoroutine(Spin());
                DataUpdater.instance.SetSpins(-1);
                slider.value = DataUpdater.instance.GetSpins();
            }
        }
    }

    public void OpenSpin()
    {
        AudioPicker.instance.buttonClicked.Play();
        SpinObject.SetActive(true);
    }

    public void CloseSpin()
    {
        AudioPicker.instance.buttonClicked.Play();
        SpinObject.SetActive(false);
        DataUpdater.instance.ToBase();
    }
}
