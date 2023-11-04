using System.Collections;
using TMPro;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speakerNameTextField;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private Animator backgroundAnimator;
    [SerializeField] private Animator textAnimator;
    [SerializeField] private Animator buttonAnimation;
    [SerializeField] private Animator miniGameButtonAnimation;
    [SerializeField] private Canvas miniGameCanvas;
    [SerializeField] private float offset;
    private bool _isPlaying;
    private bool _isCompleted;
    private int dialogueIndex;

    public SceneSetUp currentScene;

    private void Start()
    {
        _isPlaying = false;
        _isCompleted = false;
        dialogueIndex = 0;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isPlaying && dialogueIndex != currentScene.dialogues.Count)
        {
            speakerNameTextField.text = currentScene.dialogues[dialogueIndex].speaker.speakerName;
            speakerNameTextField.color = currentScene.dialogues[dialogueIndex].speaker.speakerTextColor;
            StartCoroutine(TextPrinting(currentScene.dialogues[dialogueIndex++].text));

            if (dialogueIndex == currentScene.dialogues.Count && !currentScene.isChoiceAvailable && !currentScene.isLeadingToMiniGame)
                _isCompleted = true;
            else if (dialogueIndex == currentScene.dialogues.Count && currentScene.isChoiceAvailable && !currentScene.isLastDialogue)
                buttonAnimation.SetTrigger("Show");
            else if (dialogueIndex == currentScene.dialogues.Count && currentScene.isLeadingToMiniGame && !currentScene.isLastDialogue)
                miniGameButtonAnimation.SetTrigger("Show");
        }
        else if (Input.GetMouseButtonDown(0) && _isPlaying == false && _isCompleted && !currentScene.isLastDialogue)
        {
            StartCoroutine(ShowText(currentScene.nextScene));
        }
    }

    public void TriggerMiniGame()
    {
        StartCoroutine(SwitchToMiniGame(currentScene.nextScene));
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

    public void ChangeToRightVersion()
    {
        buttonAnimation.SetTrigger("Hide");
        StartCoroutine(ShowText(currentScene.rightOptionScene));
    }

    private void HideBottomText()
    {
        speakerNameTextField.text = "";
        textField.text = "";
    }
    private IEnumerator ShowText(SceneSetUp scene)
    {
        currentScene = scene;
        HideBottomText();
        textAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(1f);
        backgroundAnimator.SetTrigger("ChangeScene");
        yield return new WaitForSeconds(1f);
        textAnimator.SetTrigger("Show");
        yield return new WaitForSeconds(1f);
        _isPlaying = false;
        _isCompleted = false;
        dialogueIndex = 0;
    }
    private IEnumerator SwitchToMiniGame(SceneSetUp scene)
    {
        currentScene = scene;
        HideBottomText();
        textAnimator.SetTrigger("Hide");
        miniGameButtonAnimation.SetTrigger("Hide");
        yield return new WaitForSeconds(3f);
        _isPlaying = false;
        _isCompleted = false;
        dialogueIndex = 0;
        miniGameCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
