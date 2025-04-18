using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace SoftHub.MagicWords
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    public class DialogueFetcher
    {
        private MonoBehaviour coroutineRunner;

        public event Action<JsonDialogueData> OnSuccess;
        public event Action<string> OnFailure;

        private string jsonUrl;

        public DialogueFetcher(MonoBehaviour runner, string url)
        {
            coroutineRunner = runner;
            jsonUrl = url;
        }

        public void Fetch()
        {
            coroutineRunner.StartCoroutine(LoadDialogue());
        }

        private IEnumerator LoadDialogue()
        {
            UnityWebRequest www = UnityWebRequest.Get(jsonUrl);
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
