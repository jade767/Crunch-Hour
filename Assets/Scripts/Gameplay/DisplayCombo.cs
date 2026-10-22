using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(TextMeshProUGUI), typeof(Animator))]
public class DisplayCombo : MonoBehaviour
{
    private TextMeshProUGUI comboText;
    private Animator animator;
    private void Awake()
    {
        comboText = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        NoteJudgement.onComboUpdated += UpdateCombo;
    }
    private void OnDisable()
    {
        NoteJudgement.onComboUpdated -= UpdateCombo;
    }

    private void UpdateCombo(int combo)
    {
        if (combo > 0) {
            comboText.text = "x" + combo;
            animator.SetTrigger("TextBounce");
        } else
        {
            comboText.text = "";
        }
    }
}
