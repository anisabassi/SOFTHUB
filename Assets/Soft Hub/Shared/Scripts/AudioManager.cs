using System;
using UnityEngine;

namespace SoftHub
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _UiAudioSource;
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

        public void PlayMouseHover()
        {
            _UiAudioSource.PlayOneShot(_mouseOverSound);
        }

        public void PlayMouseClicked()
        {
            _UiAudioSource.PlayOneShot(_mouseClickSound);
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
    }
}
