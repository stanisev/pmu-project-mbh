using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPicker : MonoBehaviour
{
    #region Singleton

    public static AudioPicker instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;

    }
    #endregion

    public AudioSource buttonClicked,
        spinnerSound;
}
