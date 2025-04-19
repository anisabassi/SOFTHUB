using UnityEngine;
using UnityEngine.UI;

namespace SoftHub.PhoenixFlame
{
    /// <summary>
    /// Manages the fire effect controlled via an Animator.
    /// Allows toggling fire on and off through UI buttons,
    /// updating the animator's boolean parameter accordingly.
    /// </summary>
    public class FireManager : MonoBehaviour
    {
        [SerializeField]private Animator _fireAnimator;
        [SerializeField] private string _firingParameter = "isFiring";
        [SerializeField] private Button _enableFireButton;
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
