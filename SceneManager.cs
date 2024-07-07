using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public int sceneToLoad = 1; // Set the scene ID to load in the Inspector if needed

    private Button playButton;

    private void Start()
    {
        // Find the Button component on the GameObject
        playButton = GetComponent<Button>();

        // Add a listener to the button's click event
        playButton.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        // Load the scene with the specified sceneToLoad ID
        SceneManager.LoadScene(sceneToLoad);
    }
}