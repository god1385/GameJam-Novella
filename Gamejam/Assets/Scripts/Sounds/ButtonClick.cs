using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public void OnButtonClick()
    {
        AudioManager.instance.PlaySfx("ClickSound");
    }
}
