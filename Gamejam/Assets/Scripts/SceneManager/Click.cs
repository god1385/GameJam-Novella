using System.Collections;
using TMPro;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speakerNameTextField;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private SceneSetUp scene;
    [SerializeField] private float offset;
    private bool _isPlaying = false;

    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TextPrinting(scene.dialogues[0].text));
        }
    }


    private IEnumerator TextPrinting(string text)
    {
        _isPlaying = true;
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
