using TMPro;
using UnityEngine;

public class ClickTrigger : MonoBehaviour
{
    [SerializeField] private string textField;
    [SerializeField] private TextMeshProUGUI trigger;

    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            trigger.text = textField;
        }
    }
}
