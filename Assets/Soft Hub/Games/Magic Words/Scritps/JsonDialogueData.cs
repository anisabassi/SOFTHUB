using System;
using System.Collections.Generic;

namespace SoftHub.MagicWords
{
    [System.Serializable]
    public class Dialogue
    {
        public string name;
        public string text;
    }

    [System.Serializable]
    public class Emoji
    {
        public string name;
        public string url;
    }

    [System.Serializable]
    public class Avatar
    {
        public string name;
        public string url;
        public string position;
    }

    [System.Serializable]
    public class JsonDialogueData
    {
        public List<Dialogue> dialogue;
        public List<Emoji> emojies;
        public List<Avatar> avatars;
    }
}
