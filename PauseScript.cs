using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public Button pauseButton;
    public Button resumeButton;
    public Button mainMenuButton;
    public GameObject pauseCanvas;

    void Start()
    {
        
        if (pauseButton != null)
            pauseButton.onClick.AddListener(PauseGame);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    public void PauseGame()
    {
       
        if (pauseCanvas != null)
            pauseCanvas.SetActive(true); 
    }

    public void ResumeGame()
    {
        
        if (pauseCanvas != null)
           pauseCanvas.SetActive(false); 
    
    }

    public void LoadMainMenu()
    {
        
        SceneManager.LoadScene(5); 
    }
}
