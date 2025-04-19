using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SoftHub
{
    /// <summary>
    /// Handles sound events for UI buttons, playing specific sounds on mouse interactions.
    /// </summary>
    public class UIButtonSoundEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        /// <summary>
        /// Called when the mouse pointer enters the UI element.
        /// Plays the mouse-over sound if SoundManager is available.
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMouseHover();
            }
        }

        /// <summary>
        /// Called when the UI element is clicked.
        /// Plays the mouse-click sound if SoundManager is available.
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMouseClicked();
            }
        }
    }
}