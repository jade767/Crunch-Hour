using UnityEngine;


[CreateAssetMenu(fileName = "New Song Info", menuName = "Scriptable Objects/Game Info")]
public class GameInfoSO : ScriptableObject
{
    public SongInfoSO currentPlayingSongInfo;
}