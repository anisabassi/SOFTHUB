using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SoftHub
{
    /// <summary>
    /// Displays the current frames per second (FPS) on a UI Text element.
    /// Automatically updates the FPS value at a defined interval.
    /// Implements a singleton pattern to persist between scenes.
    /// </summary>
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText; // Reference to the Text UI component
        [SerializeField] private float _updateInterval = 0.5f; // Time interval for updating FPS

        private float timer;
        private static FPSCounter instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Tracks elapsed time and updates the FPS display at the defined interval.
        /// </summary>
        private void Update()
        {
            // Update timer and check if we should update the FPS display
            timer += Time.deltaTime;
            if (timer >= _updateInterval)
            {
                UpdateFPSDisplay();
                timer = 0f; // Reset timer
            }
        }

        /// <summary>
        /// Calculates the current FPS and updates the UI text and color accordingly.
        /// </summary>
        private void UpdateFPSDisplay()
        {
            // Calculate FPS
            int fps = Mathf.RoundToInt(1f / Time.unscaledDeltaTime);

            // Set the FPS text
            _fpsText.text = $"{fps} FPS";

            // Update text color based on FPS
            _fpsText.color = GetColorBasedOnFPS(fps);
        }

        /// <summary>
        /// Determines the color of the FPS text based on performance range.
        /// </summary>
        /// <param name="fps">The current frames per second.</param>
        /// <returns>A color indicating performance: red (low), white (moderate), or green (high).</returns>
        private Color GetColorBasedOnFPS(int fps)
        {
            // Return color based on FPS range
            if (fps < 30)
            {
                return Color.red; // Red if FPS is low
            }
            else if (fps > 60)
            {
                return Color.green; // Green if FPS is high
            }
            else
            {
                return Color.white; // White for normal FPS
            }
        }
    }
}
