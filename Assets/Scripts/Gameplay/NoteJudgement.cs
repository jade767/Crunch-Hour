using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class NoteJudgement : MonoBehaviour
{
    [SerializeField] private GameObject TriggerLine;
    
    [Header("Judgement Distance Intervals")]
    [SerializeField] private float perfectInterval;
    [SerializeField] private float goodInterval;
    [SerializeField] private ScoreData scoreData;

    // total notes successfully hit
    private int totalNotesHit;
    // total notes passed by, whether hit or not
    private int totalNotesAdvanced;
    private bool songActive;
    private List<Transform> musicNotesInTriggerLine = new List<Transform>();

    [Header("Moving Trigger Line")]
    private float startX;
    private float startY;
    private float endX;
    [SerializeField] private int maxRows;
    [SerializeField] private float rowSpacing;
    private float lineSpeed;
    private int rowIndex;

    // maybe delete this later
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI accuracyText;

    private void OnEnable()
    {
        SongManager.onSongStarted += SongStarted;
    }

    private void OnDisable()
    {
        SongManager.onSongStarted -= SongStarted;
    }

    private void Awake()
    {
        Physics2D.callbacksOnDisable = false;
    }

    private void Start()
    {
        SongManager songManager = SongManager.instance;

        lineSpeed = songManager.LineSpeed;
        rowSpacing = songManager.RowSpacing;
        maxRows = songManager.MaxRows;
        startX = songManager.noteStartPos.x;
        startY = songManager.noteStartPos.y;
        endX = songManager.noteEndPos.x;
    }

    // lerp from start x to end x and reset position once its hit
    private float timer;
    private void Update()
    {
        if (songActive)
        {
            timer = timer + Time.deltaTime * lineSpeed;
            if (timer > 1)
            {
                timer = 0;
                rowIndex = ++rowIndex % maxRows;
            }
            float newY = startY - rowSpacing * rowIndex;
            
            Vector3 newPos = Vector2.Lerp(new Vector3(startX, newY, 0), 
                new Vector3(endX, newY, 0), timer);
            transform.position = newPos;
        }
    }

    private void SongStarted()
    {
        songActive = true;
    }

    // probably won't need miss zone since once the note exits the collider that should count as a miss anyawy
    private void NoteMissed()
    {
        scoreData.combo = 0;
        scoreData.flowstate = 1;
        accuracyText.text = "MISS";
        //AdvanceTriggerLine();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (musicNotesInTriggerLine.Count <= 0 || !context.started) return;

        Transform incomingNote = musicNotesInTriggerLine[0];
        musicNotesInTriggerLine.RemoveAt(0);
        Destroy(incomingNote.gameObject);
        
        float distance = incomingNote.transform.position.x - TriggerLine.transform.position.x;
        float absDistance = Math.Abs(distance);

        if (absDistance <= perfectInterval)
        {
            accuracyText.text = distance + ": Perfect";
            scoreData.flowstate += 0.03f;
            scoreData.score += Mathf.Floor(50 * scoreData.flowstate);
        } else if (absDistance <= goodInterval)
        {
            accuracyText.text = distance + ": Good";
            scoreData.flowstate += 0.02f;
            scoreData.score += Mathf.Floor(30 * scoreData.flowstate);
        } else
        {
            accuracyText.text = distance + ": Bad";
            scoreData.flowstate += 0.01f;
            scoreData.score += Mathf.Floor(10 * scoreData.flowstate);
        }
        
        scoreData.combo += 1;
        scoreData.CheckHighScore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MusicNote"))
        {
            musicNotesInTriggerLine.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.CompareTag("MusicNote"))
        {
            musicNotesInTriggerLine.Remove(collision.transform);
            NoteMissed();
            Destroy(collision.gameObject);
        }
    }
}
