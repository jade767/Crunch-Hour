using UnityEngine;
using System;

public class MusicNote : MonoBehaviour
{
    public Vector2 SpawnPos;
    public Vector2 RemovePos;
    public float BeatOfThisNote;
    public float BeatsShownInAdvance;

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = Vector2.Lerp(
            SpawnPos,
            RemovePos,
            (BeatsShownInAdvance - (BeatOfThisNote - SongManager.songPosInBeats)) / BeatsShownInAdvance
        );
        transform.position = new Vector3(newPos.x, newPos.y, 0);
    }

    public static Action<Transform> onNoteMissed;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MissZone"))
        {
            onNoteMissed?.Invoke(transform);
            Destroy(gameObject);
        }
    }
}
