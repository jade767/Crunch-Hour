using UnityEngine;
using System;

public class MusicNote : MonoBehaviour
{
    public Vector2 SpawnPos;
    //public Vector2 RemovePos;
    public float BeatOfThisNote;
    //public float BeatsShownInAdvance;

    // i'll need to get rid of this and replace it with a function that determines
    // the note's position on the screen based on the beat of the note
    /*
    void Update()
    {
        Vector2 newPos = Vector2.Lerp(
            SpawnPos,
            RemovePos,
            (BeatsShownInAdvance - (BeatOfThisNote - SongManager.songPosInBeats)) / BeatsShownInAdvance
        );
        transform.position = new Vector3(newPos.x, newPos.y, 0);
    }
    */
}
