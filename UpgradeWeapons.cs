using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace scgFullBodyController
{

public class UpgradeWeapons : MonoBehaviour
{
    public GunController gunController;

    
    void Start()
    {
        
     gunController=FindObjectOfType<GunController>();
    }

   
    public void OnUpgradeSniperButtonClick()
    {
       if (gunController != null)
    {
        gunController.UpgradeAssaultRifle();
    }
    else
    {
        Debug.LogError("GunController is not assigned to UpgradeWeapons.");
    }
    }

    public void OnUpgradeAssaultRifleButtonClick()
    {
        gunController.UpgradeAssaultRifle();
    }

    public void OnUpgradePistolButtonClick()
    {
        gunController.UpgradePistol();
    }

    void Update()
    {
       
    }
}
}
