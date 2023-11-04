using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootImage : MonoBehaviour
{
    private float _width;
    private float _height;

    void Start()
    {
        //Canvas.ForceUpdateCanvases();
        //RectTransform rt = GetComponent<RectTransform>();
        //_width = rt.rect.width;
        //_height = rt.rect.height;
    }

    public void UpdateSize()
    {
        RectTransform rt = GetComponent<RectTransform>();
        _width = rt.rect.width;
        _height = rt.rect.height;
    }
    public float Width { get { return _width; } }  
    public float Height { get { return _height; } }

    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        gameObject.transform.parent.GetComponent<ShootingMinigameMainScript>().clearImage(gameObject);
        Destroy(gameObject);
    }
}
