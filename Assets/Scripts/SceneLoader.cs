using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    private string destinationSceneName;
    private Animator screenTransitionAnimator;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        screenTransitionAnimator = GetComponent<Animator>();
    }

    // default scene transition
    public void GoToScene(string sceneName)
    {
        destinationSceneName = sceneName;
        screenTransitionAnimator.SetTrigger("FadeBlackBegin");
    }

    public void GoToScene(string sceneName, TransitionType screenTransitionType)
    {
        destinationSceneName = sceneName;
        switch (screenTransitionType)
        {
            case TransitionType.FadeBlack:
                screenTransitionAnimator.SetTrigger("FadeBlackBegin");
                break;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (screenTransitionAnimator)
            screenTransitionAnimator.SetTrigger("FadeBlackEnd");
    }

    public void OnTransitionEnded()
    {
        SceneManager.LoadScene(destinationSceneName);
    }
    public enum TransitionType
    {
        FadeBlack,
    }
}
