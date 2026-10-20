using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongSelect : MonoBehaviour
{
    public SongInfoSO selectedSong;
    [SerializeField] private GameInfoSO gameInfo;
    [SerializeField] private TextMeshProUGUI songNameText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI durationText;

    public void OnEnable()
    {
        SongEntry.onSelectSong += SelectedSong;
    }
    public void OnDisable()
    {
        SongEntry.onSelectSong -= SelectedSong;
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

    }
}
