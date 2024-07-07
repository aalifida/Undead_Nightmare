using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace scgFullBodyController
{
    public class GunManager : MonoBehaviour
    {
        public GameObject[] weapons;
        public Animator anim;
        public OffsetRotation oRot;
        public float swapTime;
        int index = 0;

        public Button weapon1Button;
        public Button weapon2Button;
        public Button weapon3Button;

        void Start()
        {
            weapon1Button.onClick.AddListener(() => SwapWeapon(0));
            weapon2Button.onClick.AddListener(() => SwapWeapon(1));
            weapon3Button.onClick.AddListener(() => SwapWeapon(2));

            foreach (GameObject weapon in weapons)
            {
                weapon.SetActive(false);
                weapon.GetComponent<GunController>().swapping = true;
            }

            weapons[index].SetActive(true);
            Invoke("SetSwappedWeaponPositions", .567f + .25f);
        }

        void SwapWeapon(int newIndex)
        {
            if (newIndex != index && newIndex < weapons.Length)
            {
                if (!weapons[index].GetComponent<GunController>().firing && !weapons[index].GetComponent<GunController>().swapping
                    && !weapons[index].GetComponent<GunController>().aiming && weapons[index].GetComponent<GunController>().aimFinished
                    && !weapons[index].GetComponent<GunController>().reloading && !weapons[index].GetComponent<GunController>().cycling)
                {
                    index = newIndex;
                    Invoke("SwapWeapons", swapTime);
                    foreach (GameObject weapon in weapons)
                    {
                        weapon.GetComponent<GunController>().swapping = true;
                    }
                    anim.SetBool("putaway", true);
                }
            }
        }

        void SwapWeapons()
        {
            foreach (GameObject weapon in weapons)
            {
                weapon.SetActive(false);
            }

            weapons[index].SetActive(true);
            Invoke("SetSwappedWeaponPositions", .567f + .25f);

            if (weapons[index].GetComponent<GunController>().Weapon == GunController.WeaponTypes.Rifle)
            {
                oRot.rifle = true;
                oRot.pistol = false;
            }
            else if (weapons[index].GetComponent<GunController>().Weapon == GunController.WeaponTypes.Pistol)
            {
                oRot.rifle = false;
                oRot.pistol = true;
            }

            anim.SetBool("putaway", false);
        }

        void SetSwappedWeaponPositions()
        {
            if (!weapons[index].GetComponent<GunController>().aimPosSet)
            {
                weapons[index].GetComponent<GunController>().initiliazeOrigPositions();
                weapons[index].GetComponent<GunController>().aimPosSet = true;
            }

            foreach (GameObject weapon in weapons)
            {
                weapon.GetComponent<GunController>().swapping = false;
            }
        }
    
    }
}
