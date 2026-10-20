using Unity.VectorGraphics;
using UnityEngine;

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
        sceneLoader.GoToScene(destination);
    }
}
