using UnityEngine;
using UnityEngine.UI;

public class RegistrationMenuHandler : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas loginCanvas;
    public Canvas registrationCanvas;

    public Button loginButton;
    public Button registrationButton;

    void Start()
    {
        // Initialize the canvases
        EnableMainCanvas();
        DisableLoginCanvas();
        DisableRegistrationCanvas();

        // Assign click events to buttons
        loginButton.onClick.AddListener(EnableLoginCanvas);
        registrationButton.onClick.AddListener(EnableRegistrationCanvas);
    }

    void EnableMainCanvas()
    {
        mainCanvas.gameObject.SetActive(true);
    }

    void DisableMainCanvas()
    {
        mainCanvas.gameObject.SetActive(false);
    }

    void EnableLoginCanvas()
    {
        DisableMainCanvas();
        loginCanvas.gameObject.SetActive(true);
    }

    void DisableLoginCanvas()
    {
        loginCanvas.gameObject.SetActive(false);
    }

    void EnableRegistrationCanvas()
    {
        DisableMainCanvas();
        registrationCanvas.gameObject.SetActive(true);
    }

    void DisableRegistrationCanvas()
    {
        registrationCanvas.gameObject.SetActive(false);
    }

    public void OnBackButtonClicked()
    {
        EnableMainCanvas();
        DisableLoginCanvas();
        DisableRegistrationCanvas();
    }
}
