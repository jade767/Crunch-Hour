using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string destinationScene;

    public void GoToScene()
    {
        SceneManager.LoadScene(destinationScene);
    }
}
