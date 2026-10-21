using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class SongEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    private SongInfoSO songInfo;
    public SongInfoSO SongInfo
    {
        get
        {
            return songInfo;
        }
        set
        {
            songInfo = value;
            nameText.text = songInfo.songName;
            difficultyText.text = songInfo.difficultyLevel.ToString();
        }
    }
    
    public static event Action<SongInfoSO> onSelectSong;
    public void OnSelected()
    {
        SfxManager.OnSelect?.Invoke();
        onSelectSong?.Invoke(songInfo);
    }
}
