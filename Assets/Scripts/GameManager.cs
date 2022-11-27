using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Questions[] questions;
    // Start is called before the first frame update
    private static List<Questions> unansweredQuestions;

    private Questions currentQuestion;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private Text trueAnswerText, wrongAnswerText;

    [SerializeField]
    private GameObject trueAnswerButton, wrongAnswerButton;

    int trueansweredCount, wrongansweredCount;

    int totalScore;

    [SerializeField]
    private GameObject resultPanel;

    ResultManager resultManager;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Questions>();
        }
        RandomlySelectQuestion();

        trueansweredCount = 0;
        wrongansweredCount = 0;
        totalScore = 0;
    }

    void RandomlySelectQuestion()
    {
        wrongAnswerButton.GetComponent<RectTransform>().DOLocalMoveX(270f, 2f);
        trueAnswerButton.GetComponent<RectTransform>().DOLocalMoveX(-270f, 2f);

        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        questionText.text = currentQuestion.question;

        if (currentQuestion.itIsTrue)
        {
            trueAnswerText.text = "True Answer";
            wrongAnswerText.text = "False Answer";
        }
        else
        {
            trueAnswerText.text = "False Answer";
            wrongAnswerText.text = "True Answer";
        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(2f);
        
        if (unansweredQuestions.Count <= 0)
        {
            resultPanel.SetActive(true);

            resultManager = Object.FindObjectOfType<ResultManager>();
            resultManager.ShowResult(trueansweredCount, wrongansweredCount, totalScore);
        }
        else
        {
            RandomlySelectQuestion();
        }
    }

    public void TrueAnswer()
    {
        if (currentQuestion.itIsTrue)
        {
            trueansweredCount++;
            totalScore += 50;
        }
        else
        {
            wrongansweredCount++;
        }
        wrongAnswerButton.GetComponent<RectTransform>().DOLocalMoveX(1000f, 2f);
        StartCoroutine(TransitionToNextQuestion());
    } 
    public void WrongAnswer()
    {
        if (!currentQuestion.itIsTrue)
        {
            trueansweredCount++;
            totalScore += 50;
        }
        else
        {
            wrongansweredCount++;
        }
       
        trueAnswerButton.GetComponent<RectTransform>().DOLocalMoveX(-1000f, 2f);
        StartCoroutine(TransitionToNextQuestion());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
        resultPanel.SetActive(false);
        TransitionToNextQuestion();
    }
}
