using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShootImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool isBad;
    [SerializeField] private List<Sprite> imageListPNG;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.transform.parent.GetComponent<ShootingMinigameMainScript>().ClearImage(gameObject);
        Destroy(gameObject);

        if ((isBad && eventData.button == PointerEventData.InputButton.Left) ||
            (!isBad && eventData.button == PointerEventData.InputButton.Right))
        {
            score.AddScore();
        }
        else
        {
            score.DecreaseScore();
        }
    }
    private void Start()
    {
        score = GetComponentInParent<ScriptScore>();
        if (isBad)
        {
            gameObject.GetComponent<Image>().sprite = imageListPNG[Random.Range(0, imageListPNG.Count)];
        }
    }
}
