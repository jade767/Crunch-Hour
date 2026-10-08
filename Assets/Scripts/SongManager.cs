using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

    [Header("Game Object References")]
    [SerializeField] private GameObject MusicNotePrefab;
    [SerializeField] private GameObject NoteMissZone;
    [SerializeField] private GameObject StartLine;
    

    // -- Song Position Tracking --

    // the current position of the song (in seconds)
    private float songPosition;
    // the current position of the song (in beats)
    public static float songPosInBeats;
    // the duration of a beat
    private float secPerBeat;
    // how much time (in seconds) has passed since the song started
    private float dsptimesong;

    // -- Song Information -- (should probably make a scriptable object for each song)

    [Header("Song Information")]
    // beats per minute of the song
    [SerializeField] private float bpm;
    // keep all the positon-in-beats of notes in the song
    [SerializeField] private float[] notes;
    // the index of the next note to be spawned
    private int nextIndex = 0;

    // Note Positioning
    [Header("Note Positioning")]
    [SerializeField] private int maxRows;
    [SerializeField] private float originYPos;
    [SerializeField] private float rowSpacing;
    [SerializeField] private int MaxNotesOnLine;
    private int noteCountOnLine;
    private int rowIndex = -1;
    private Vector2 noteStartPos;
    private Vector2 noteEndPos;

    private void OnEnable()
    {
        NoteJudgement.onStartNewNoteRow += StartNewNoteRow;
    }

    private void OnDisable()
    {
        NoteJudgement.onStartNewNoteRow -= StartNewNoteRow;
    }

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        // not sure if i need to make this a singleton
        instance = this;
    }

    private void Start()
    {
        // calculate how many seconds is one beat
        secPerBeat = 60f / bpm;

        // record the time when the song starts
        dsptimesong = (float) AudioSettings.dspTime;

        StartNewNoteRow();
        // start the song
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        // calculate the position in seconds
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);

        // calculate the position in beats
        songPosInBeats = songPosition / secPerBeat;

        if (nextIndex < notes.Length && notes[nextIndex] < songPosInBeats + beatsShownInAdvance)
        {
            GameObject obj = Instantiate(MusicNotePrefab);
            // initialize the fields of the music note
            MusicNote musicNote = obj.GetComponent<MusicNote>();
            musicNote.SpawnPos = noteStartPos;
            musicNote.RemovePos = noteEndPos;
            musicNote.BeatOfThisNote = notes[nextIndex];
            musicNote.BeatsShownInAdvance = beatsShownInAdvance;

            onNoteCreated?.Invoke(obj);

            noteCountOnLine++;
            if (noteCountOnLine >= MaxNotesOnLine)
            {
                noteCountOnLine = 0;
                StartNewNoteRow();
            }

            nextIndex++;
        }
    }

    private void StartNewNoteRow()
    {
        rowIndex = ++rowIndex % maxRows;
        float yPos = originYPos - rowIndex * rowSpacing;
        noteStartPos = new Vector2(StartLine.transform.position.x, yPos);
        noteEndPos = new Vector2(NoteMissZone.transform.position.x, yPos);
    }
}
