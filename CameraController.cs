using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scgFullBodyController
{
    public class CameraController : MonoBehaviour
    {
        public float Sensitivity = 0.005f;
        public float SensitivityMultiplier = 0.5f; // Adjust this value to control the rotation speed
        public float minPitch = -30f;
        public float maxPitch = 60f;
        public Transform parent;
        public Transform boneParent;

        private float pitch = 0f;
        [HideInInspector] public float yaw = 0f;
        [HideInInspector] public float relativeYaw = 0f;
        public bool isSwiping=false;


        void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void LateUpdate()
        {
            CameraRotate();
            transform.position = boneParent.position;
        }

        void CameraRotate()
        {
            if (isSwiping)
            {
                Touch touch = Input.GetTouch(0);
                relativeYaw = touch.deltaPosition.x * Sensitivity * SensitivityMultiplier;
                pitch -= touch.deltaPosition.y * Sensitivity * SensitivityMultiplier;
                pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
                yaw += relativeYaw;
                transform.eulerAngles = new Vector3(pitch, yaw, 0f);
            }
        }
        public void swip(){
            Debug.Log("Swipe");
            isSwiping=true;
        }
        public void notswiping(){
            Debug.Log("Swipe Stop");

            isSwiping=false;
        }
    }
}
