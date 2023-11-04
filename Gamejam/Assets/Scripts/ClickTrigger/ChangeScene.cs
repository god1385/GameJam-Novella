using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Click scene;
    [SerializeField] private Image image;
    public void Change()
    {
        image.sprite = scene.currentScene.backgroundImage;
    }
}
