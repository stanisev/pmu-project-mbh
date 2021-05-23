using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrueOrFalseQuizManager : MonoBehaviour
{
    public GameObject TrueOrFalseContainer;

    public TMP_Text currentQuestionTextDisplay;

    public GameObject AnswerContainer;
    public TMP_Text displayAnswerText;

    public int totalNumberOfQuestions = 9;
    private int maxNumberOfQuestions = 9;

    private List<string> totalQuestions = new List<string>();
    private List<string> allAnswers = new List<string>();

    private string yourAnswer = "False";
    private string correctAnswer = "True";

    int totalAnsweredQuestions = 0;

    public GameObject AwardsContainer;
    public TMP_Text AwardsText;
    private int totalCorrectAnswers = 0;

    public GameObject energyImage;
    public GameObject correctImage, wrongImage;

    public GameObject energyContainer;

    public GameObject dScreen;

    public TMP_Text currentEnergy;

    private int questionLevel = 1;

    public void Update()
    {
        currentEnergy.text = DataUpdater.instance.GetEnergy().ToString();
    }
    public void getAllQuestions()
    {
        for(int i=1; i <= totalNumberOfQuestions; i++)
        {
            totalQuestions.Add(GameMultiLang.GetTraduction("TF" + i));
            allAnswers.Add(GameMultiLang.GetTraduction("TF" + i + "A"));
        }
    }

    public void generateRandomQuestion()
    {

        int pickRandomQuestion = Random.Range(0, totalNumberOfQuestions);

        currentQuestionTextDisplay.text = totalQuestions[pickRandomQuestion];

        setCorrectAnswer(allAnswers[pickRandomQuestion]);

        if (totalNumberOfQuestions < 2)
        {                    
            Invoke("GenerateAwards", 0.3f);
        }

        totalQuestions.RemoveAt(pickRandomQuestion);
        allAnswers.RemoveAt(pickRandomQuestion);

        

    }

    public void setCorrectAnswer(string correct)
    {
        correctAnswer = correct;
    }

    public void CheckIfTrueOrFalse()
    {
        AnswerContainer.SetActive(true);

        if(correctAnswer.Equals(yourAnswer))
        {
            energyImage.SetActive(false);
            correctImage.SetActive(true);
            displayAnswerText.text = "";

            totalCorrectAnswers++;
            DataUpdater.instance.SetCorrectAnswer(1);
            
        } 
        else
        {
            energyImage.SetActive(true);
            wrongImage.SetActive(true);
            displayAnswerText.text = "-1";
            DataUpdater.instance.SetEnergy(-1);

            DataUpdater.instance.SetWrongAnswer(1);

        }

        totalAnsweredQuestions++;
        totalNumberOfQuestions--;

        generateRandomQuestion();

        Invoke("closeAnswerContainer", 1.5f);
    }
    public void clickTrue()
    {
        yourAnswer = "True";

        CheckIfTrueOrFalse();

    }

    public void clickFalse()
    {
        yourAnswer = "False";

        CheckIfTrueOrFalse();

    }

    public void closeAnswerContainer()
    {
        correctImage.SetActive(false);
        wrongImage.SetActive(false);
        AnswerContainer.SetActive(false);
    }

    public void CloseTrueOrFalseContainer()
    {
        TrueOrFalseContainer.SetActive(false);
        totalCorrectAnswers = 0;
        DataUpdater.instance.ToBase();
    }

    public void OpenTrueOrFalseContainer()
    {
        if(!(DataUpdater.instance.GetEnergy() <= 0))
        {
            TrueOrFalseContainer.SetActive(true);
            ResetQuestions();
        }
        else
        {
            OpenEnergyContainer();
        }
       
    }
    public void OpenEnergyContainer()
    {
        energyContainer.SetActive(true);
    }
    public void CloseEnergyContainer()
    {
        energyContainer.SetActive(false);
    }
    public void ResetQuestions()
    {
        totalNumberOfQuestions = maxNumberOfQuestions;
        getAllQuestions();
        generateRandomQuestion();
    }

    public void GenerateAwards()
    {

        AwardsContainer.SetActive(true);

        int prize = totalCorrectAnswers * 100 * questionLevel;
        DataUpdater.instance.SetCoins(prize);

        AwardsText.text = "+ " + prize;

    }

    public void CloseAwardsContainer()
    {
        AwardsContainer.SetActive(false);
        Invoke("CloseTrueOrFalseContainer", 0.01f);
    }

    public void OpenDificultyScreen()
    {
        TrueOrFalseContainer.SetActive(true);
        dScreen.SetActive(true);
    }

    public void CloseDificultyScreen()
    {
        TrueOrFalseContainer.SetActive(false);
        dScreen.SetActive(false);
    }

    public void ChoseEasy()
    {
        if (!(DataUpdater.instance.GetEnergy() < 3))
        {
            this.questionLevel = 1;
            totalNumberOfQuestions = 5;
            maxNumberOfQuestions = 5;
            dScreen.SetActive(false);
            OpenTrueOrFalseContainer();
        }
        else
        {
            OpenEnergyContainer();
        }

    }
    public void ChoseMedium()
    {
        if (!(DataUpdater.instance.GetEnergy() < 5))
        {
            this.questionLevel = 2;
            totalNumberOfQuestions = 7;
            maxNumberOfQuestions = 7;
            dScreen.SetActive(false);
            OpenTrueOrFalseContainer();
        }
        else
        {
            OpenEnergyContainer();
        }

    }
    public void ChoseHard()
    {
        if (!(DataUpdater.instance.GetEnergy() < 8))
        {
            this.questionLevel = 3;
            totalNumberOfQuestions = 9;
            maxNumberOfQuestions = 9;
            dScreen.SetActive(false);
            OpenTrueOrFalseContainer();
        }
        else
        {
            OpenEnergyContainer();
        }

    }
}
