using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Call this function when the "Play Again" button is pressed
    public void PlayAgain()
    {
     
        LoadSceneByIndex(1);
    }

    // Call this function when the "Menu" button is pressed
    public void MainMenu()
    {
        
        LoadSceneByIndex(5);
    }
}

