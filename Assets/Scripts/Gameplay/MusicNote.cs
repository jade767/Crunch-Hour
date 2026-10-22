using UnityEngine;
using System;

public class MusicNote : MonoBehaviour
{
    [Header("Note Sprites")]
    [SerializeField] private Sprite defaultNote;
    [SerializeField] private Sprite startHoldNote;
    [SerializeField] private Sprite endHoldNote;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isHoldNote;
    private bool isHoldEnd;
    public bool IsHoldNote
    {
        get
        {
            return isHoldNote;
        }
        set
        {
            isHoldNote = value;
            if (isHoldNote)
            {
                spriteRenderer.sprite = startHoldNote;
            }
        }
    }
    public bool IsHoldEnd
    {
        get {

            return isHoldEnd;
        }
        set
        {
            isHoldEnd = value;
            if (isHoldEnd)
            {
                spriteRenderer.sprite = endHoldNote;
            }
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void DestroyHitNote()
    {
        animator.SetTrigger("Hit");
    }

    public void DestroyMissNote()
    {
        animator.SetTrigger("Miss");
    }

    public void AnimationEnded()
    {
        Destroy(gameObject);
    }
}
