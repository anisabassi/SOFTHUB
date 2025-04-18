using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SoftHub
{
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

        // Call this method to load a scene by its name
        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
