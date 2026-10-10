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
    public static Action onStartNewNoteRow;
    
    [Header("Judgement Distance Intervals")]
    [SerializeField] private float perfectInterval;
    [SerializeField] private float goodInterval;

    // total notes successfully hit
    private int totalNotesHit;
    // total notes passed by, whether hit or not
    private int totalNotesAdvanced;
    private List<Transform> musicNotesInTriggerLine = new List<Transform>();

    [Header("Moving Trigger Line")]
    // trigger line moving though idk if we should keep this
    [SerializeField] private bool movingTriggerLine;
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private int maxColumns;
    [SerializeField] private float originTriggerXPos;
    [SerializeField] private float columnSpacing;
    private int columnIndex = 0;

    // maybe delete this later
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI accuracyText;

    private void OnEnable()
    {
        MusicNote.onNoteMissed += NoteMissed;
    }

    private void OnDisable()
    {
        MusicNote.onNoteMissed -= NoteMissed;
    }

    private void NoteMissed(Transform noteTransform)
    {
        scoreData.combo = 0;
        scoreData.flowstate = 1;
        accuracyText.text = "MISS";
        AdvanceTriggerLine();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (musicNotesInTriggerLine.Count <= 0 || !context.started) return;

        Transform incomingNote = musicNotesInTriggerLine[0];
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
        musicNotesInTriggerLine.RemoveAt(0);
        Destroy(incomingNote.gameObject);
        AdvanceTriggerLine();
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
        if (collision.CompareTag("MusicNote"))
        {
            musicNotesInTriggerLine.Remove(collision.transform);
        }
    }

    private void AdvanceTriggerLine()
    {
        if (movingTriggerLine)
        {
            columnIndex = ++columnIndex % maxColumns;
            TriggerLine.transform.position = new Vector3(originTriggerXPos + columnIndex * columnSpacing, 
                TriggerLine.transform.position.y, 0);
        }
    }
}
