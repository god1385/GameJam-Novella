using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDayScene", menuName = "NewDayScene") ]
[System.Serializable]
public class SceneSetUp : ScriptableObject
{
    public List<Dialogue> dialogues;
    public Sprite backgroundImage;
    public SceneSetUp nextScene;

    [System.Serializable]
    public struct Dialogue
    {
        public string text;
        public Speaker speaker;
    }
}
