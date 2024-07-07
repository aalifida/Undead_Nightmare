using UnityEngine;
using UnityEngine.UI;

namespace scgFullBodyController
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        public Slider healthSlider;

        private ThirdPersonCharacter m_Character;
        private Transform m_Cam;
        private Vector3 m_CamForward;
        private Vector3 m_Move;
        private bool m_Jump;

        float sprintSpeed=5;
         float walkSpeed=4;
         float crouchSpeed=3;
        [HideInInspector] public bool slide;
        public float slideTime;
        bool crouchToggle = false;
        bool proneToggle = false;
        bool crouch = false;
        bool prone = false;
        bool sprint = false;
        bool canVault = false;
        bool vaulting = false;
        bool strafe;
        bool forwards;
        bool backwards;
        bool right;
        bool left;
        public float vaultCancelTime;
        float horizontalInput;
        float verticalInput;
        public float sensitivity;
        public GameObject cameraController;
        GameObject collidingObj;

        // UI buttons
        public Button jumpButton;
        public Button crouchButton;
        public Button sprintButton;
        public Button proneButton;
        public Button upArrowButton;
        public Button downArrowButton;
        public Button leftArrowButton;
        public Button rightArrowButton;
        public Button fireButton;
        public Button reloadButton;
        public Button aimButton;

        private bool firing;
        private bool reloading;
        private bool aiming;
        

        private void Start()
        {
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            }

            m_Character = GetComponent<ThirdPersonCharacter>();

            // Set up UI button click events
            jumpButton.onClick.AddListener(OnJumpButtonClick);
            crouchButton.onClick.AddListener(OnCrouchButtonClick);
            sprintButton.onClick.AddListener(OnSprintButtonClick);
            proneButton.onClick.AddListener(OnProneButtonClick);
            upArrowButton.onClick.AddListener(OnUpArrowButtonClick);
            downArrowButton.onClick.AddListener(OnDownArrowButtonClick);
            leftArrowButton.onClick.AddListener(OnLeftArrowButtonClick);
            rightArrowButton.onClick.AddListener(OnRightArrowButtonClick);
            fireButton.onClick.AddListener(OnFireButtonClick);
            reloadButton.onClick.AddListener(OnReloadButtonClick);
            aimButton.onClick.AddListener(OnAimButtonClick);
        }

        public void OnUpArrowButtonClick()
{
  
    forwards = true;
    backwards = false;
   
}

public void OnDownArrowButtonClick()
{
    Debug.Log("down");
    forwards = false;
    backwards = true;
  
  
}

public void OnArrowButtonRelease()
{
    forwards = false;
    backwards = false;
    strafe = false;
    right = false;
    left = false;

    m_Move = Vector3.zero;
    m_Character.Move(m_Move, crouch, m_Jump, slide, vaulting);
}

        public void OnLeftArrowButtonClick()
        {
            strafe = true;
            right = false;
            left = true;
        }

        public void OnRightArrowButtonClick()
        {
            strafe = true;
            right = true;
            left = false;
        }

      

        public void OnJumpButtonClick()
        {
            m_Jump = true;
        }

        public void OnCrouchButtonClick()
        {
            crouchToggle = !crouchToggle;
            if (crouchToggle)
            {
                crouch = true;
            }
            else
            {
                crouch = false;
            }
        }

        public void OnSprintButtonClick()
        {
            sprint = true;
        }

        public void OnProneButtonClick()
        {
            proneToggle = !proneToggle;
            if (proneToggle)
            {
                prone = true;
            }
            else
            {
                prone = false;
            }
        }

        public void OnFireButtonClick()
        {
            firing = true;
        }

        public void OnReloadButtonClick()
        {
            if (!reloading)
            {
                reloading = true;
                // Perform any additional logic or UI updates as needed
            }
        }

        public void OnAimButtonClick()
        {
            if (!aiming)
            {
                aiming = true;
                // Perform any additional logic or UI updates as needed
            }
            else if (aiming)
            {
                aiming = false;
                // Perform any additional logic or UI updates as needed
            }
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.transform.tag == "vaultObject")
            {
                collidingObj = col.gameObject;
                canVault = true;
            }
            else
            {
                canVault = false;
            }
        }

        void OnCollisionExit(Collision col)
        {
            canVault = false;
        }

        private void Update()
        {
            // Set input values based on UI buttons
            verticalInput = forwards ? 1f : (backwards ? -1f : 0f);
            horizontalInput = strafe ? (right ? 1f : (left ? -1f : 0f)) : 0f;

            if (firing && canVault)
            {
                collidingObj.GetComponent<Collider>().enabled = false;
                firing = false;
                vaulting = true;
                Invoke("vaultCancel", vaultCancelTime);
            }

            m_Character.updateLate(m_Move, crouch, prone, vaulting, forwards, backwards, strafe, horizontalInput, verticalInput);
        }

        private void FixedUpdate()
        {
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = verticalInput * m_CamForward * walkSpeed;

            if (sprint) m_Move *= sprintSpeed;

            m_Character.Move(m_Move, crouch, m_Jump, slide, vaulting);

            m_Character.HandleGroundMovement(crouch, m_Jump, slide);
            m_Jump = false;
        }

        void vaultCancel()
        {
            vaulting = false;
            canVault = false;
            collidingObj.GetComponent<Collider>().enabled = true;
        }

        void slideCancel()
        {
            slide = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("FirstAid"))
            {
                healthSlider.value += 30f;
                Destroy(other.gameObject);
            }
            else if(other.CompareTag("Ammo")){
                
            }
        }
    }
}
