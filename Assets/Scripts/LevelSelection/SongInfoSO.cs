using UnityEngine;

[CreateAssetMenu(fileName = "New Song Info", menuName = "ScriptableObjects/SongInfo")]
public class SongInfoSO : ScriptableObject
{
    public string songName;
    public DifficultyLevel difficultyLevel;
    public AudioClip audioClip;
    public float[] notes;
}

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}