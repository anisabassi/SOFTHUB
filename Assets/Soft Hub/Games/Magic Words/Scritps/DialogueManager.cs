using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace SoftHub.MagicWords
{
    /// <summary>
    /// Handles loading and displaying dialogue lines with avatars from a remote JSON source.
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private string _jsonUrl = "https://private-624120-softgamesassignment.apiary-mock.com/v2/magicwords";
        [SerializeField] private List<DialogueData> CurrentData = new List<DialogueData>();
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private TextMeshProUGUI _avatarText;
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;

        private int _dialogueIndex = 0;

        void Start()
        {
            DialogueFetcher fetcher = new DialogueFetcher(this, _jsonUrl);

            fetcher.OnSuccess += OnDialogueLoaded;
            fetcher.OnFailure += OnDialogueFailed;

            fetcher.LoadDialogueData();

            _previousButton.onClick.AddListener(PreviousDialogue);
            _nextButton.onClick.AddListener(NextDialogue);
        }

        private void OnDestroy()
        {
            _previousButton.onClick.RemoveListener(PreviousDialogue);
            _nextButton.onClick.RemoveListener(NextDialogue);
        }

        /// <summary>
        /// Called when dialogue JSON is successfully loaded and parsed.
        /// </summary>
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

        /// <summary>
        /// Called if loading dialogue fails (e.g. network error).
        /// </summary>
        private void OnDialogueFailed(string error)
        {
            Debug.LogError("Dialogue load failed: " + error);
        }


        /// <summary>
        /// Converts {emoji} placeholders in dialogue text to TextMeshPro sprite tags.
        /// </summary>
        private string ReplaceEmojiTags(string input)
        {
            return Regex.Replace(input, @"\{(.*?)\}", m => $"<sprite name=\"{m.Groups[1].Value}\">");
        }

        /// <summary>
        /// Wraps avatar names with sprite tags for display in TextMeshPro.
        /// </summary>
        private string AddAvatarTags(string name)
        {
            return $"<sprite name=\"{name}\">";
        }

        /// <summary>
        /// Moves forward in the dialogue sequence.
        /// </summary>
        private void NextDialogue()
        {
            if (_dialogueIndex < CurrentData.Count - 1)
            {
                _dialogueIndex++;
                UpdateDialogueUI();
            }
        }

        /// <summary>
        /// Moves backward in the dialogue sequence.
        /// </summary>
        private void PreviousDialogue()
        {
            if (_dialogueIndex > 0)
            {
                _dialogueIndex--;
                UpdateDialogueUI();
            }
        }

        /// <summary>
        /// Updates the dialogue and avatar UI based on the current index.
        /// </summary>
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
