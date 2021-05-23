using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Questionnaire : MonoBehaviour
{
    public GameObject Q_Object, A_Object;

    public TMP_Text currentQuestionTextDisplay;
    public TMP_Text AwardsText;

    public int totalNumberOfQuestions = 4;
    private int maxNumberOfQuestions = 4;

    private List<string> totalQuestions = new List<string>();
    private List<string> allAnswers = new List<string>();

    private string yourAnswer = "9999";
    private string correctAnswer = "0000";

    public Button[] buttonTexts;
    public GameObject help50;
    public GameObject[] disabledButtons;

    private string a1, a2, a3;

    private int totalCorrect = 0;
    public int questionLevel = 1;

    public GameObject energyUI;
    public GameObject rankUI;
    public void getAllQuestions()
    {
        for (int i = 1; i <= totalNumberOfQuestions; i++)
        {
            totalQuestions.Add(GameMultiLang.GetTraduction("QF" + i));
            allAnswers.Add(GameMultiLang.GetTraduction("QF" + i + "A"));
        }
    }
    public void generateRandomQuestion()
    {
        ActivateButtons();
        help50.SetActive(true);
        Debug.Log(totalNumberOfQuestions);
        int pickRandomQuestion = Random.Range(0, totalNumberOfQuestions);

        currentQuestionTextDisplay.text = totalQuestions[pickRandomQuestion];

        setCorrectAnswer(allAnswers[pickRandomQuestion]);

        if (totalNumberOfQuestions < Random.Range(2, 4))
        {
            Invoke("GenerateAwards", 0.01f);
        }

        totalQuestions.RemoveAt(pickRandomQuestion);
        allAnswers.RemoveAt(pickRandomQuestion);
    }
    public void setCorrectAnswer(string correct)
    {
        correctAnswer = correct;
        buttonTexts[0].GetComponentInChildren<TMP_Text>().text = correctAnswer;

        int ran1 = Random.Range(int.Parse(correctAnswer) - 20, int.Parse(correctAnswer) - 1);
        a1 = ran1.ToString();

        int ran2 = Random.Range(int.Parse(correctAnswer) - 25, int.Parse(correctAnswer) - 1);
        a2 = ran2.ToString();

        int ran3 = Random.Range(int.Parse(correctAnswer) - 30, int.Parse(correctAnswer) - 1);
        a3 = ran3.ToString();

        buttonTexts[1].GetComponentInChildren<TMP_Text>().text = a1;
        buttonTexts[2].GetComponentInChildren<TMP_Text>().text = a2;
        buttonTexts[3].GetComponentInChildren<TMP_Text>().text = a3;
    }

    public void RandomButtonPosition()
    {
        Vector2 b1 = buttonTexts[0].transform.position;
        Vector2 b2 = buttonTexts[1].transform.position;
        Vector2 b3 = buttonTexts[2].transform.position;
        Vector2 b4 = buttonTexts[3].transform.position;

        int random = Random.Range(0, 4);

        switch(random)
        {
            case 0:
                buttonTexts[0].transform.position = b4;
                buttonTexts[1].transform.position = b3;
                buttonTexts[2].transform.position = b2;
                buttonTexts[3].transform.position = b1;
                break;
            case 1:
                buttonTexts[0].transform.position = b3;
                buttonTexts[1].transform.position = b2;
                buttonTexts[2].transform.position = b1;
                buttonTexts[3].transform.position = b4;
                break;
            case 2:
                buttonTexts[0].transform.position = b2;
                buttonTexts[1].transform.position = b1;
                buttonTexts[2].transform.position = b4;
                buttonTexts[3].transform.position = b3;
                break;
            case 3:
                buttonTexts[0].transform.position = b1;
                buttonTexts[1].transform.position = b2;
                buttonTexts[2].transform.position = b3;
                buttonTexts[3].transform.position = b4;
                break;
            default:
                buttonTexts[0].transform.position = b4;
                buttonTexts[1].transform.position = b3;
                buttonTexts[2].transform.position = b2;
                buttonTexts[3].transform.position = b1;
                break;
        }


    }

    public void closeRankUI()
    {
        rankUI.SetActive(false);
    }
    public void startIt()
    {
        if (DataUpdater.instance.GetRank() < 2)
        {
            rankUI.SetActive(true);
        } else
        {
            A_Object.SetActive(false);

            RandomButtonPosition();
            totalCorrect = 0;
            ResetQuestions();
            Q_Object.SetActive(true);
        }

    }

    public void button_click_1()
    {
        totalCorrect++;
        totalNumberOfQuestions--;
        generateRandomQuestion();
    }
    public void button_click_2()
    {
        totalNumberOfQuestions--;
        generateRandomQuestion();
    }
    public void button_click_3()
    {
        totalNumberOfQuestions--;
        generateRandomQuestion();
    }
    public void button_click_4()
    {
        totalNumberOfQuestions--;
        generateRandomQuestion();
    }
    public void Open()
    {
        totalNumberOfQuestions--;
        ResetQuestions();
        Q_Object.SetActive(true);
    }
    public void Close()
    {
        Q_Object.SetActive(false);
    }

    public void GenerateAwards()
    {

        A_Object.SetActive(true);

        int prize = totalCorrect * 100 * questionLevel;
        DataUpdater.instance.SetCoins(prize);
        DataUpdater.instance.SetCorrectAnswer(totalCorrect);

        AwardsText.text = "+ " + prize;

    }
    public void ResetQuestions()
    {
        totalNumberOfQuestions = maxNumberOfQuestions;
        getAllQuestions();
        generateRandomQuestion();
    }
    public void A_Object_Close() 
    {
        A_Object.SetActive(false);
        Close();
    }

    public void ActivateButtons()
    {
        foreach(GameObject b in disabledButtons)
        {
            b.SetActive(true);
        }
    }

    public void HelpFifty()
    {
        if(!(DataUpdater.instance.GetEnergy() < 2))
        {
            DataUpdater.instance.SetEnergy(-2);

            disabledButtons[0].SetActive(false);
            disabledButtons[1].SetActive(false);
            help50.SetActive(false);

            UseTwoEnergy();
        }
    }
    #region Help Fifty Energy
    public void UseTwoEnergy()
    {
        energyUI.SetActive(true);
        Invoke("CloseUseTwoEnergy", 0.5f);
    }
    public void CloseUseTwoEnergy()
    {
        energyUI.SetActive(false);
    }
    #endregion
}
