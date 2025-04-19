using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SoftHub
{
    /// <summary>
    /// Handles scene loading through a UI button.
    /// Automatically binds to the Button component on the same GameObject,
    /// and loads the specified scene when clicked.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]private string _sceneName;
        private Button _currentButton;

        private void Start()
        {
            _currentButton = this.GetComponent<Button>();
            if(_currentButton != null)
            {
                _currentButton.onClick.AddListener(LoadScene);
            }
        }

        private void OnDestroy()
        {
            if(_currentButton !=null)
            {
                _currentButton.onClick.RemoveListener(LoadScene);
            }
        }

        /// <summary>
        /// Loads the scene with the name specified in the _sceneName field.
        /// Make sure the scene is added to the build settings.
        /// </summary>
        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
