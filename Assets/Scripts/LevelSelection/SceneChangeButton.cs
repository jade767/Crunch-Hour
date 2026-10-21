using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;

public class SceneChangeButton : MonoBehaviour
{
    public string destination;
    public void OnClick()
    {
        SceneLoader sceneLoader = FindAnyObjectByType<SceneLoader>();
        if (!sceneLoader)
        {
            Debug.LogError("Could not find SceneLoader in scene.");
        }
        SfxManager.OnSelect?.Invoke();
        sceneLoader.GoToScene(destination);
    }
}
