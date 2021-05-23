using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProfileController : MonoBehaviour
{
    public GameObject ProfileUI;
    public Animator ProfileUIAnimator;

    public AudioListener audioListener;

    public GameObject on, off;
    public void Start()
    {
    }

    #region Profile UI
    public void OpenProfileUI()
    {
        ProfileUI.SetActive(true);
        ProfileUIAnimator.SetTrigger("profile-ui");
    }

    public void CloseProfileUI()
    {
        ProfileUIAnimator.SetTrigger("profile-close");
        Invoke("CloseProfileCanvas", 0.3f);
        
    }

    public void CloseProfileCanvas()
    {
        ProfileUI.SetActive(false);
    }
    #endregion

    public void TurnOnMusic()
    {
        audioListener.enabled = true;
        on.SetActive(false);
        off.SetActive(true);
    }

    public void TurnOffMusic()
    {
        audioListener.enabled = false;
        off.SetActive(false);
        on.SetActive(true);
    }
}
