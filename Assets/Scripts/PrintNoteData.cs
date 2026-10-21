using UnityEngine;

public class PrintNoteData : MonoBehaviour
{
    [SerializeField] GameInfoSO gameData;
    public void OnClick()
    {
        SongInfoSO songInfo = gameData.currentPlayingSongInfo;
        string output = "";
        for (int i = 0; i < songInfo.notes.Length; i++)
        {
            Note note = songInfo.notes[i];
            string noteInfoString = "Note " + i + " - " + "BeatPos = " + note.beatPos;
            noteInfoString = noteInfoString + ", isHoldNote = " + note.isHoldNote.ToString(); 
            noteInfoString = noteInfoString + ", isHoldEnd = " + note.isHoldEnd.ToString();
            output = output + noteInfoString + "\n";
        }
        Debug.Log(output);
    }
}
