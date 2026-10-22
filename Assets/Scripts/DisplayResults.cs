using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI perfectCounterText;
    [SerializeField] private TextMeshProUGUI goodCounterText;
    [SerializeField] private TextMeshProUGUI badCounterText;
    [SerializeField] private TextMeshProUGUI missCounterText;
    [SerializeField] private Sprite letterA;
    [SerializeField] private Sprite letterB;
    [SerializeField] private Sprite letterC;
    [SerializeField] private Sprite letterD;
    [SerializeField] private Sprite letterF;
    [SerializeField] private Image letterGrade;
    

    private void Awake()
    {
        if (!scoreData)
        {
            Debug.LogError("Score data object not set, cannot display results.");
        } else
        {
            float percentageScore = Mathf.Floor(scoreData.score / scoreData.maxScore * 100);
            scoreText.text = "Total Score: " + scoreData.score + " (" 
                + percentageScore.ToString() + "%)";
            perfectCounterText.text = "PERFECT: " + scoreData.perfectNotesHit;
            goodCounterText.text = "GOOD: " + scoreData.goodNotesHit;
            badCounterText.text = "BAD: " + scoreData.badNotesHit;
            missCounterText.text = "MISS: " + scoreData.missedNotes;
            letterGrade.enabled = true;
            if (percentageScore >= 70)
            {
                letterGrade.sprite = letterA;
            } else if (percentageScore >= 60)
            {
                letterGrade.sprite = letterB;
            } else if (percentageScore >= 50)
            {
                letterGrade.sprite = letterC;
            } else if (percentageScore > 40)
            {
                letterGrade.sprite = letterD;
            } else
            {
                letterGrade.sprite = letterF;
            }
        }
    }
}
