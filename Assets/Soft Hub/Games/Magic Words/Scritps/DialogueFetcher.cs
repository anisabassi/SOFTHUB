using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace SoftHub.MagicWords
{
    /// <summary>
    /// Responsible for fetching dialogue data from a remote JSON source.
    /// </summary>
    public class DialogueFetcher
    {
        public event Action<JsonDialogueData> OnSuccess;
        public event Action<string> OnFailure;

        private MonoBehaviour _coroutineRunner;
        private string _jsonUrl;

        /// <summary>
        /// Constructor for initializing the DialogueFetcher with a coroutine runner and target URL.
        /// </summary>
        /// <param name="runner">MonoBehaviour instance used to run coroutines.</param>
        /// <param name="url">URL pointing to the JSON dialogue data.</param>
        public DialogueFetcher(MonoBehaviour runner, string url)
        {
            _coroutineRunner = runner;
            _jsonUrl = url;
        }

        /// <summary>
        /// Begins the fetch operation for the dialogue data.
        /// </summary>
        public void LoadDialogueData()
        {
            _coroutineRunner.StartCoroutine(LoadDialogue());
        }

        /// <summary>
        /// Coroutine that performs the web request and handles response parsing or error reporting.
        /// </summary>
        private IEnumerator LoadDialogue()
        {
            UnityWebRequest www = UnityWebRequest.Get(_jsonUrl);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                JsonDialogueData data = JsonUtility.FromJson<JsonDialogueData>(www.downloadHandler.text);
                OnSuccess?.Invoke(data);
            }
            else
            {
                OnFailure?.Invoke(www.error);
            }
        }
    }
}
