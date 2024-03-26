using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Static reference to the SceneController instance
    public static SceneController instance;

    private void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set the instance to this object and make it persistent across scenes
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }

    // Method to load the next level in the build settings
    public void NextLevel()
    {
        // Get the index of the next scene in the build settings and load it asynchronously
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }

    // Method to load a specific scene by name
    public void LoadScene(string sceneName)
    {
        // Load the scene with the given name asynchronously
        SceneManager.LoadSceneAsync(sceneName);
    }
    
    // Method to load the previous level in the build settings
    public void PreviousLevel()
    {
        // Get the index of the previous scene in the build settings and load it asynchronously
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 3;
        SceneManager.LoadSceneAsync(previousSceneIndex);
    }
}
