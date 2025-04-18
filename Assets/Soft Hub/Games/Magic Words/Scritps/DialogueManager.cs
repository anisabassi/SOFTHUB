using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace SoftHub.MagicWords
{
    public class DialogueManager : MonoBehaviour
    {
        public string jsonUrl = "https://private-624120-softgamesassignment.apiary-mock.com/v2/magicwords";
        public List<DialogueData> CurrentData = new List<DialogueData>();

        public TextMeshProUGUI _dialogueText;
        public TextMeshProUGUI _avatarText;

        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;

        private int _dialogueIndex = 0;

        void Start()
        {
            DialogueFetcher fetcher = new DialogueFetcher(this, jsonUrl);

            fetcher.OnSuccess += OnDialogueLoaded;
            fetcher.OnFailure += OnDialogueFailed;

            fetcher.Fetch();

            _previousButton.onClick.AddListener(PreviousDialogue);
            _nextButton.onClick.AddListener(NextDialogue);
        }

        private void OnDestroy()
        {
            _previousButton.onClick.RemoveListener(PreviousDialogue);
            _nextButton.onClick.RemoveListener(NextDialogue);
        }

        private void OnDialogueLoaded(JsonDialogueData data)
        {
            var currentLines = data.dialogue;
            foreach (var line in currentLines)
            {
                DialogueData newItem = new DialogueData
                {
                    DialogueLine = ReplaceEmojiTags(line.text),
                    Avatar = AddAvatarTags(line.name)
                };

                CurrentData.Add(newItem);
            }

            _dialogueIndex = 0;
            UpdateDialogueUI();
        }

        private void OnDialogueFailed(string error)
        {
            Debug.LogError("Dialogue load failed: " + error);
        }

        private string ReplaceEmojiTags(string input)
        {
            return Regex.Replace(input, @"\{(.*?)\}", m => $"<sprite name=\"{m.Groups[1].Value}\">");
        }

        private string AddAvatarTags(string name)
        {
            return $"<sprite name=\"{name}\">";
        }

        public void NextDialogue()
        {
            if (_dialogueIndex < CurrentData.Count - 1)
            {
                _dialogueIndex++;
                UpdateDialogueUI();
            }
        }

        public void PreviousDialogue()
        {
            if (_dialogueIndex > 0)
            {
                _dialogueIndex--;
                UpdateDialogueUI();
            }
        }

        private void UpdateDialogueUI()
        {
            if (CurrentData.Count == 0 || _dialogueIndex < 0 || _dialogueIndex >= CurrentData.Count)
                return;

            var line = CurrentData[_dialogueIndex];
            _avatarText.text = line.Avatar;
            _dialogueText.text = line.DialogueLine;

            // Enable or disable buttons based on index
            _previousButton.interactable = _dialogueIndex > 0;
            _nextButton.interactable = _dialogueIndex < CurrentData.Count - 1;
        }
    }

    [System.Serializable]
    public class DialogueData
    {
        public string DialogueLine;
        public string Avatar;
    }
}
