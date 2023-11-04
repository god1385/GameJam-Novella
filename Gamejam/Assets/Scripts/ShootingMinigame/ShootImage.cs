using Unity.VisualScripting;
using UnityEngine;

public class ShootImage : MonoBehaviour
{
    private ScriptScore score;
    private float _width;
    private float _height;

    public float Width { get { return _width; } }
    public float Height { get { return _height; } }

    public void UpdateSize()
    {
        RectTransform rt = GetComponent<RectTransform>();
        _width = rt.rect.width;
        _height = rt.rect.height;
    }

    public void OnMouseDown()
    {
        gameObject.transform.parent.GetComponent<ShootingMinigameMainScript>().ClearImage(gameObject);
        Destroy(gameObject);
        score.AddScore();
    }
    private void Start()
    {
        score = GetComponentInParent<ScriptScore>();
    }
}
