using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "NewSpeaker")]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Color speakerTextColor;
    public Sprite speakerSprite;
    public bool mainCharacter;
}
