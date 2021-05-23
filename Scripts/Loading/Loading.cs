using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Loading : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI showProgress;

    private string userName;

    public void Start()
    {

       // LoadLevel(2);
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            showProgress.text = (progress * 100f).ToString("f0") + " %";
            yield return null;
        }


    }

    public void userNameInputValidation(string input)
    {
        this.userName = input;

        
    }
}
