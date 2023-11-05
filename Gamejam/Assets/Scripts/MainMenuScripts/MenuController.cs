using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEndScript : MonoBehaviour
{
    [SerializeField] private GameObject buttonPlay;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject buttonExit;
    [SerializeField] private GameObject buttonSettings;
    [SerializeField] private GameObject buttonSettingsExit;
    [SerializeField] private GameObject SoundSlider;
    [SerializeField] private GameObject SettingsImage;
    private bool _isHidden = true;
    public void StartGame()
    {
        mainCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
        AudioManager.instance.PlayMusic("StartDayTheme");
    }
    public void Settings()
    {
        _isHidden = !_isHidden;
        buttonPlay.SetActive(_isHidden);
        buttonExit.SetActive(_isHidden);
        buttonSettings.SetActive(_isHidden);
        SettingsImage.SetActive(!_isHidden);
        SoundSlider.SetActive(!_isHidden);
        buttonSettingsExit.SetActive(!_isHidden);
    }
    public void Exit()
    {
        Application.Quit(); 
    }
}
