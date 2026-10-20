using UnityEngine;

[CreateAssetMenu(fileName = "New Song Info", menuName = "Scriptable Objects/Song Info")]
public class SongInfoSO : ScriptableObject
{
    public string songName;
    public DifficultyLevel difficultyLevel;
    public AudioClip audioClip;
    public Note[] notes;
}

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}