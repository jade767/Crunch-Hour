using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongSelect : MonoBehaviour
{
    public SongInfoSO selectedSong;
    [SerializeField] private GameInfoSO gameInfo;
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private TextMeshProUGUI songNameText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI durationText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private SongInfoSO firstSelectedSong;

    public void OnEnable()
    {
        SongEntry.onSelectSong += SelectedSong;
    }
    public void OnDisable()
    {
        SongEntry.onSelectSong -= SelectedSong;
    }

    private void Start()
    {
        if (firstSelectedSong)
            selectedSong = firstSelectedSong;
            SelectedSong(selectedSong);
    }

    private void SelectedSong(SongInfoSO songInfo)
    {
        selectedSong = songInfo;
        gameInfo.currentPlayingSongInfo = selectedSong;
        
        // update song details tab
        songNameText.text = selectedSong.songName;
        difficultyText.text = "Difficulty: " + selectedSong.difficultyLevel;
        int songLength = Mathf.FloorToInt(selectedSong.audioClip.length);
        int songMinutes = songLength / 60;
        int songSeconds = songLength % 60;
        durationText.text = "Song Length: " + songMinutes + ":" + songSeconds;
        highScoreText.text = "Highest Score: " + scoreData.highscore;

    }
}
