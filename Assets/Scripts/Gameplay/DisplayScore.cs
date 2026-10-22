using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    private Slider scoreBar;
    [SerializeField] private ScoreData scoreData;

    private void Awake()
    {
        scoreBar = GetComponentInChildren<Slider>();
    }
    private void OnEnable()
    {
        SongManager.onSongStarted += CalculateMaxScore;
        NoteJudgement.onScoreUpdated += UpdateScoreBar;
    }

    private void OnDisable()
    {
        SongManager.onSongStarted -= CalculateMaxScore;
        NoteJudgement.onScoreUpdated -= UpdateScoreBar;
    }

    private void UpdateScoreBar(float currentScore)
    {
        scoreBar.value = currentScore / scoreData.maxScore;
    }

    private void CalculateMaxScore(SongInfoSO song)
    {
        int noteCount = song.notes.Length;
        float maxSongScore = 50 * noteCount;
        /*
        for (int i = 0; i < noteCount; i++)
        {
            maxSongScore += 50 * (1 + 0.03f * i);
        }
        */
        scoreData.maxScore = maxSongScore;
    }

    
}
