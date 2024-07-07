using UnityEngine;
using TMPro;
using Firebase.Auth;

public class DisplayUserInfo : MonoBehaviour
{
    public TextMeshProUGUI userinfo;
    public TextMeshProUGUI userCoins;
    public TextMeshProUGUI userHighscore;
    public TextMeshProUGUI Message;

    string username;
    int highScore;
    int coins;

    void Update()
    {
        coins = PlayerPrefs.GetInt("coins", 0);
        highScore = PlayerPrefs.GetInt("highScore", 0);
        username = PlayerPrefs.GetString("PlayerUsername", "guest");

        userinfo.text = "Username: " + username;
        userCoins.text = coins.ToString();
        userHighscore.text = "Highscore: " + highScore;
    }


    public void OnPistolUpgradeClick()
    { if(coins<8000){
        int reqcoins=8000-coins;
        Message.text="Not Enough Coins You Need "+reqcoins+" More Coins";
           Invoke("ClearText",3);
       }
       else
       {
        Message.text="Sniper Upgraded Sucessfully";
           Invoke("ClearText",3);
       }
    }
    
    public void OnSniperUpgradeClick()
    {
      if(coins<10000){
        int reqcoins=10000-coins;
        Message.text="Not Enough Coins You Need "+reqcoins+" More Coins";
           Invoke("ClearText",3);
       }
       else
       {
        Message.text="Sniper Upgraded Sucessfully";
           Invoke("ClearText",3);
        
       }
    
    }

   
    public void OnARUpgradeClick()
    { if(coins<15000){
        int reqcoins=15000-coins;
        Message.text="Not Enough Coins You Need "+reqcoins+" More Coins";
         Invoke("ClearText",3);
       }
       else
       {
        Message.text="Sniper Upgraded Sucessfully";
        Invoke("ClearText",3);
       }
        
    }
    void ClearText(){
      Message.text="";
    }
}
