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
    

    private void Awake()
    {
        if (!scoreData)
        {
            Debug.LogError("Score data object not set, cannot display results.");
        } else
        {
            scoreText.text = "Total Score: " + scoreData.score;
            perfectCounterText.text = "PERFECT: " + scoreData.perfectNotesHit;
            goodCounterText.text = "GOOD: " + scoreData.goodNotesHit;
            badCounterText.text = "BAD: " + scoreData.badNotesHit;
            missCounterText.text = "MISS: " + scoreData.missedNotes;
        }
    }
}
