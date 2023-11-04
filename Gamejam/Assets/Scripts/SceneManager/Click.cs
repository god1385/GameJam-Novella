using System.Collections;
using TMPro;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speakerNameTextField;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private SceneSetUp scene;
    [SerializeField] private float offset;
    private bool _isPlaying;
    private bool _isCompleted;
    private int dialogueIndex;

    private void Start()
    {
        _isPlaying = false;
        _isCompleted = false;
        dialogueIndex = 0;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isPlaying && dialogueIndex != scene.dialogues.Count)
        {
            speakerNameTextField.text = scene.dialogues[dialogueIndex].speaker.speakerName;
            speakerNameTextField.color = scene.dialogues[dialogueIndex].speaker.speakerTextColor;
            StartCoroutine(TextPrinting(scene.dialogues[dialogueIndex++].text));

            if (dialogueIndex != scene.dialogues.Count)
                _isCompleted = true;
        }
        else if (Input.GetMouseButtonDown(0) && _isPlaying == false && _isCompleted)
        {
            
        }
    }

    private void ChangeScene(SceneSetUp scene)
    {
        scene = scene.nextScene;
        _isPlaying = false;
        _isCompleted = false;
        dialogueIndex = 0;
    }
    private IEnumerator TextPrinting(string text)
    {
        _isPlaying = true;
        textField.text = "";
        char[] letters = text.ToCharArray();

        for (int i = 0; i < letters.Length; i++)
        {
            char c = letters[i];
            textField.text += c;
            yield return new WaitForSeconds(offset);
        }

        _isPlaying = false;
    }
}
