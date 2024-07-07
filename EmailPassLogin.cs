using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase;
using UnityEngine.SceneManagement; // Added this line for SceneManager

public class EmailPassLogin : MonoBehaviour
{
    public TMP_InputField LoginEmail;
    public TMP_InputField loginPassword;
    public TMP_InputField SignupUsername;
    public TMP_InputField SignupEmail;
    public TMP_InputField SignupPassword;
    public TMP_InputField SignupPasswordConfirm;
    public TextMeshProUGUI logTxt1;
    public TextMeshProUGUI logTxt2;
    public TextMeshProUGUI logTxt3;

    private void Awake() {
        
   
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
        });
    }

    public void SignUp()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        string email = SignupEmail.text;
        string password = SignupPassword.text;
        string username = SignupUsername.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                DisplayLogMsg("Error creating user", logTxt1);
                DisplayLogMsg("Error creating user", logTxt2);
                DisplayLogMsg("Error creating user", logTxt3);
                return;
            }

            AuthResult result = task.Result;
            UserProfile profile = new UserProfile { DisplayName = username };
            result.User.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(profileTask =>
            {
                if (profileTask.IsCanceled || profileTask.IsFaulted)
                {
                    DisplayLogMsg("Error updating user profile", logTxt1);
                    DisplayLogMsg("Error updating user profile", logTxt2);
                    DisplayLogMsg("Error updating user profile", logTxt3);
                    return;
                }

                SignupEmail.text = "";
                SignupPassword.text = "";
                SignupPasswordConfirm.text = "";
                SignupUsername.text = "";

                DisplayLogMsg("Sign up Successful", logTxt1);
                DisplayLogMsg("Sign up Successful", logTxt2);
                DisplayLogMsg("Sign up Successful", logTxt3);
            });
        });
    }

    private void StoreUsernameInPlayerPrefs(string username)
    {
        PlayerPrefs.SetString("PlayerUsername", username);
        PlayerPrefs.Save();
    }

    public void Login()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        string email = LoginEmail.text;
        string password = loginPassword.text;

        Credential credential = EmailAuthProvider.GetCredential(email, password);
        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                DisplayLogMsg("Error signing in", logTxt1);
                DisplayLogMsg("Error signing in", logTxt2);
                DisplayLogMsg("Error signing in", logTxt3);
                return;
            }

            AuthResult result = task.Result;

            StoreUsernameInPlayerPrefs(result.User.DisplayName);

            DisplayLogMsg("Log in Successful", logTxt1);
            DisplayLogMsg("Log in Successful", logTxt2);
            DisplayLogMsg("Log in Successful", logTxt3);
            SceneManager.LoadScene(5); 
        });
    }

    IEnumerator ShowLogMsg(string msg, TextMeshProUGUI targetText)
    {
        targetText.text = msg;
        yield return new WaitForSeconds(5f);
        targetText.text = "";
    }

    void DisplayLogMsg(string msg, TextMeshProUGUI targetText)
    {
        StartCoroutine(ShowLogMsg(msg, targetText));
    }
}
