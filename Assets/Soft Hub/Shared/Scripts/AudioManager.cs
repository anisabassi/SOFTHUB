using System;
using UnityEngine;

namespace SoftHub
{
    /// <summary>
    /// Manages UI audio feedback such as mouse hover and click sounds.
    /// Implements a singleton pattern to ensure a single instance persists across scenes.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _uiAudioSource;
        [SerializeField] private AudioClip _mouseOverSound;
        [SerializeField] private AudioClip _mouseClickSound;

        private static AudioManager instance;

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError("AudioManager instance is null. Ensure it's in the scene and set to DontDestroyOnLoad.");
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Persist across scenes
            }
            else
            {
                Destroy(gameObject); // Destroy duplicate instances
            }
        }


        /// <summary>
        /// Plays the mouse hover sound effect.
        /// </summary>
        public void PlayMouseHover()
        {
            _uiAudioSource.PlayOneShot(_mouseOverSound);
        }

        /// <summary>
        /// Plays the mouse click sound effect.
        /// </summary>
        public void PlayMouseClicked()
        {
            _uiAudioSource.PlayOneShot(_mouseClickSound);
        }

    }
}
