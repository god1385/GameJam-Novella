using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDayScene", menuName = "NewDayScene") ]
[System.Serializable]
public class SceneSetUp : ScriptableObject
{
    public List<Dialogue> dialogues;
    public Sprite backgroundImage;
    public SceneSetUp nextScene;
    public SceneSetUp rightOptionScene;
    public SceneSetUp leftOptionScene;
    public bool isChoiceAvailable;
    public bool isLeadingToMiniGame;
    public bool isLastDialogue;

    [System.Serializable]
    public struct Dialogue
    {
        [TextAreaAttribute]
        public string text;
        public Speaker speaker;
    }
}
