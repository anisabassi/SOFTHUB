using UnityEngine;
using UnityEngine.UI;

namespace SoftHub.PhoenixFlame
{
    public class FireManager : MonoBehaviour
    {
        [Header("Fire Animator Settings")]
        [Tooltip("Animator component controlling the fire effects.")]
        [SerializeField]private Animator _fireAnimator;

        [Header("Animation Parameters")]
        [Tooltip("Boolean parameter in the Animator that controls the fire state.")]
        [SerializeField] private string _firingParameter = "isFiring"; // The bool parameter in Animator

        [Header("UI Buttons")]
        [Tooltip("Button to enable the fire.")]
        [SerializeField] private Button _enableFireButton;

        [Tooltip("Button to disable the fire.")]
        [SerializeField] private Button _disableFireButton;

        private void Start()
        {
            if (_enableFireButton != null && _disableFireButton != null)
            {
                _enableFireButton.onClick.AddListener(StartFire);
                _disableFireButton.onClick.AddListener(KillFire);

                // Set initial button states
                UpdateButtonStates(true);
            }
            else
            {
                Debug.LogError("UI Buttons not assigned.");
            }
        }

        /// <summary>
        /// Starts the fire by setting the isFiring parameter to true.
        /// </summary>
        public void StartFire()
        {
            _fireAnimator.SetBool(_firingParameter, true);
            UpdateButtonStates(true);
        }

        /// <summary>
        /// Stops the fire by setting the isFiring parameter to false.
        /// </summary>
        public void KillFire()
        {
            _fireAnimator.SetBool(_firingParameter, false);
            UpdateButtonStates(false);
        }

        /// <summary>
        /// Updates the button states to ensure only one button is active at a time.
        /// </summary>
        /// <param name="isFireActive">Indicates if the fire is currently active.</param>
        private void UpdateButtonStates(bool isFireActive)
        {
            _enableFireButton.interactable = !isFireActive; 
            _disableFireButton.interactable = isFireActive;
        }
    }
}
