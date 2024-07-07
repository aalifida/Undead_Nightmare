using UnityEngine;

namespace scgFullBodyController
{
    public class AmmoIncreasing : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GunController gc = other.gameObject.GetComponentInChildren<GunController>();
                if (gc != null)
                {
                    gc.IncreaseAmmo();
                    Debug.Log("Ammo increased!");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogError("GunController component not found on the player object.");
                }
            }
        }
    }
}
