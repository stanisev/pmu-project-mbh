using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text rightAnswerText;
    public TMP_Text wrongAnswerText;
    public TMP_Text trophieText;

    public void NewScoreElement(string _username, int _right, int _wrong, int _trophie)
    {
        usernameText.text = _username;
        rightAnswerText.text = _right.ToString();
        wrongAnswerText.text = _wrong.ToString();
        trophieText.text = _trophie.ToString();
    }
}
