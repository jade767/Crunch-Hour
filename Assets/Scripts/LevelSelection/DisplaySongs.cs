using UnityEngine;

public class DisplaySongs : MonoBehaviour
{
    [SerializeField] private GameObject songEntryPrefab;
    [SerializeField] private Transform contentTransform;
    private void Awake()
    {
        SongInfoSO[] allSongInfos = Resources.LoadAll<SongInfoSO>("Songs");
        foreach (SongInfoSO songInfo in allSongInfos)
        {
            SongEntry entry = Instantiate(songEntryPrefab, contentTransform).GetComponent<SongEntry>();
            entry.SongInfo = songInfo;
        }
    }
}
