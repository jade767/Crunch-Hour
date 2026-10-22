using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VectorGraphics;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class SongManager : MonoBehaviour
{
    /* 
    most of this code is from this tutorial 
    https://www.gamedeveloper.com/programming/music-syncing-in-rhythm-games
    if you want to read it
    */
    public static SongManager instance;
    public int beatsShownInAdvance;

    // Component References
    private AudioSource musicSource;

    // -- Events --
    public static Action<GameObject> onNoteCreated;
    public static Action<SongInfoSO> onSongStarted; // should contain a songobject type at some point

    [Header("Game Object References")]
    [SerializeField] private GameObject MusicNotePrefab;
    [SerializeField] private TextMeshProUGUI countdownText;
    

    // -- Song Position Tracking --

    // the current position of the song (in seconds)
    private float songPosition;
    // the current position of the song (in beats)
    public static float songPosInBeats;
    // the duration of a beat
    private float secPerBeat;
    // how much time (in seconds) has passed since the song started
    private float dsptimesong;
    private bool songActive;

    // -- Song Information -- (should probably make a scriptable object for each song)

    [Header("Song Information")]
    [SerializeField] private GameInfoSO gameInfo;
    [SerializeField] private SongInfoSO defaultSong;
    // beats per minute of the song
    [SerializeField] private float bpm;
    // keep all the positon-in-beats of notes in the song
    [SerializeField] private Note[] notes;
    // the index of the next note to be spawned
    private int nextIndex = 0;

    // Note Positioning
    [Header("Note Positioning")]
    
    [SerializeField] private float originYPos;
    [SerializeField] private int MaxNotesOnLine;
    [SerializeField] private float noteStartX;
    public int MaxRows;
    public float RowSpacing;
    public float LineSpeed;
    private float unitsPerBeat;

    //private int rowIndex = -1;
    public Vector2 noteStartPos;
    public Vector2 noteEndPos;
    private float rowLength;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        // not sure if i need to make this a singleton
        instance = this;

        // calculate how many seconds is one beat
        secPerBeat = 60f / bpm;

        // calculate beats per row
        rowLength = noteEndPos.x - noteStartPos.x;
        unitsPerBeat = secPerBeat * (rowLength * LineSpeed);
    }

    private void Update()
    {
        if (!songActive) return;
        // calculate the position in seconds
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);

        // calculate the position in beats
        songPosInBeats = songPosition / secPerBeat;

        if (songActive && !musicSource.isPlaying)
        {
            songActive = false;
            SceneLoader.instance.GoToScene("ResultsScreen", SceneLoader.TransitionType.FadeBlack);
        } else if (nextIndex < notes.Length && notes[nextIndex].beatPos < songPosInBeats + beatsShownInAdvance)
        {
            GameObject obj = Instantiate(MusicNotePrefab);
            // initialize the fields of the music note
            MusicNote musicNote = obj.GetComponent<MusicNote>();
            float newX = notes[nextIndex].beatPos * unitsPerBeat;
            int rowIndex = (int) (newX / rowLength) % MaxRows;
            obj.transform.position = noteStartPos + new Vector2(newX % rowLength, 
                -RowSpacing * rowIndex);
            musicNote.IsHoldNote = notes[nextIndex].isHoldNote;
            musicNote.IsHoldEnd = notes[nextIndex].isHoldEnd;
            //musicNote.BeatOfThisNote = notes[nextIndex];
            //musicNote.BeatsShownInAdvance = beatsShownInAdvance;

            onNoteCreated?.Invoke(obj);

            nextIndex++;
        }
    }

    private void Start()
    {
        StartCoroutine(StartSongCountdown());
    }

    IEnumerator StartSongCountdown()
    {
        Animator countdownAnimator = countdownText.GetComponent<Animator>();
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            countdownAnimator.SetTrigger("TextBounceLarge");
            yield return new WaitForSeconds(1);
        }
        countdownText.text = "";
        
        StartSong();
    }

    public void StartSong()
    {
        SongInfoSO selectedSong = gameInfo.currentPlayingSongInfo;
        if (!selectedSong)
        {
            Debug.Log("Song info not found in GameInfo, switching to default song");
            selectedSong = defaultSong;
            gameInfo.currentPlayingSongInfo = defaultSong;
        }
        musicSource.generator = selectedSong.audioClip;
        notes = selectedSong.notes;

        onSongStarted?.Invoke(selectedSong);
        
        // record the time when the song starts
        dsptimesong = (float) AudioSettings.dspTime;

        // play the song
        songActive = true;
        musicSource.Play();
    }
}

[Serializable]
public class Note
{
    public float beatPos;
    public bool isHoldNote;
    public bool isHoldEnd;
}
