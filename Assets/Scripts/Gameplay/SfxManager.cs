using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SfxManager : MonoBehaviour
{
    public AudioSource select;
    public AudioSource note;
    public AudioSource music;

    public static UnityAction OnSelect;
    public static UnityAction OnNoteHit;

    private static SfxManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        UpdateMusic(SceneManager.GetActiveScene());
    }

    private void OnEnable()
    {
        OnSelect += SelectSFX;
        OnNoteHit += NoteSFX;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        OnSelect -= SelectSFX;
        OnNoteHit -= NoteSFX;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void SelectSFX()
    {
        select.Play();
    }
    private void NoteSFX()
    {
        note.Play();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateMusic(scene);
    }

    private void UpdateMusic(Scene scene)
    {
        if (scene.name == "MainMenu" || scene.name == "SongSelect" || scene.name == "Tutorial")
        {
            if (!music.isPlaying)
            {
                music.Play();
            }
        }
        else 
        { 
            music.Stop();
        }
    }
}
