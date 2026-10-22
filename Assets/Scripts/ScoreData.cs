using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Scriptable Objects/Score Data")]
public class ScoreData : ScriptableObject
{
    public float score;
    //score multiplier
    public float flowstate;
    public float combo;
    public float highscore;
    public float maxScore;
    public int perfectNotesHit;
    public int goodNotesHit;
    public int badNotesHit;
    public int missedNotes;
    //A-F score cutoffs, cutoffs will have to be updated later to make any sence
    public float ACutOff = 30000;
    public float BCutOff = 25000;
    public float CCutOff = 15000;
    public float DCutOff = 10000;
    public float FCutOff = 5000;
    

    private void OnEnable()
    {
        ResetScore();
        highscore = PlayerPrefs.GetFloat("HighScore", 0f);
        SongManager.onSongStarted += StartSong;
    }

    private void OnDisable()
    {
        SongManager.onSongStarted -= StartSong;
    }
    public void CheckHighScore()
    {
        if (score > highscore)
        {
            highscore = score;

            PlayerPrefs.SetFloat("HighScore", highscore);
            PlayerPrefs.Save();
        }
    }


    private void StartSong(SongInfoSO song)
    {
        ResetScore();
    }
    
    public void ResetScore()
    {
        score = 0f;
        //flowstate = 1f;
        combo = 0f;
        perfectNotesHit = 0;
        goodNotesHit = 0;
        badNotesHit = 0;
        missedNotes = 0;
    }
}
