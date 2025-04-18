using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SoftHub
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText; // Reference to the Text UI component
        [SerializeField] private float updateInterval = 0.5f; // Time interval for updating FPS

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

        private void Update()
        {
            // Update timer and check if we should update the FPS display
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                UpdateFPSDisplay();
                timer = 0f; // Reset timer
            }
        }

        private void UpdateFPSDisplay()
        {
            // Calculate FPS
            int fps = Mathf.RoundToInt(1f / Time.unscaledDeltaTime);

            // Set the FPS text
            _fpsText.text = $"{fps} FPS";

            // Update text color based on FPS
            _fpsText.color = GetColorBasedOnFPS(fps);
        }

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
