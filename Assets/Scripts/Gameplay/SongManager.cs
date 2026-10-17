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
    public static Action onSongStarted; // should contain a songobject type at some point

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
    private bool songActive;

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

        if (nextIndex < notes.Length && notes[nextIndex] < songPosInBeats + beatsShownInAdvance)
        {
            GameObject obj = Instantiate(MusicNotePrefab);
            // initialize the fields of the music note
            MusicNote musicNote = obj.GetComponent<MusicNote>();
            float newX = notes[nextIndex] * unitsPerBeat;
            int rowIndex = (int) (newX / rowLength) % MaxRows;
            obj.transform.position = noteStartPos + new Vector2(newX % rowLength, 
                -RowSpacing * rowIndex);
            //musicNote.BeatOfThisNote = notes[nextIndex];
            //musicNote.BeatsShownInAdvance = beatsShownInAdvance;

            onNoteCreated?.Invoke(obj);

            nextIndex++;
        }
    }

    public void StartSong()
    {
        SongSelect songSelect = FindAnyObjectByType<SongSelect>();
        if (!songSelect)
        {
            Debug.Log("Could not find SongSelect object in scene.");
            return;
        }
        musicSource.generator = songSelect.selectedSong.audioClip;

        onSongStarted?.Invoke();
        
        // record the time when the song starts
        dsptimesong = (float) AudioSettings.dspTime;

        // play the song
        musicSource.Play();
        songActive = true;
    }
}
