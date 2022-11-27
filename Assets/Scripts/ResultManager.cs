using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Text trueCountText, wrongCountText, totalScoreText;

    [SerializeField]
    private GameObject leftStar, middleStar, rightStar;

    public void ShowResult(int trueCount, int wrongCount, int totalScore)
    {
        trueCountText.text = trueCount.ToString();
        wrongCountText.text = wrongCount.ToString();
        totalScoreText.text = totalScore.ToString();

        leftStar.SetActive(false);
        middleStar.SetActive(false);
        rightStar.SetActive(false);
        
        if (trueCount == 1)
        {
            leftStar.SetActive(true);
           
        }
        else if (trueCount == 2)
        {            
            leftStar.SetActive(true);
            rightStar.SetActive(true); 
        }
        else if (trueCount == 3)
        {
            leftStar.SetActive(true);
            middleStar.SetActive(true);
            rightStar.SetActive(true);
        }

    }

}
