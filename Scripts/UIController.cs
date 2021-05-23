using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    #region Singleton

    public static UIController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void Start()
    {
        
    }

    public Sprite[] titleScreens;
    public Image currentTitleScreen;

    public TMP_Text guestButton;
    public TMP_Text loginButton;

    public void ChangeTitleScreen(int index)
    {
        currentTitleScreen.sprite = titleScreens[index];
    }

    public void ChangeButtonsTitleScreenText(int index)
    {
        string[] guestKeys = { "Гост", "Guest" };
        string[] googleKeys = { "Гугъл", "Google" };

        switch (index)
        {
            case 0: guestButton.text = guestKeys[0];
                    loginButton.text = googleKeys[0];
                    break;

            case 1:
                    guestButton.text = guestKeys[1];
                    loginButton.text = googleKeys[1];
                    break;
        }
    }
}
