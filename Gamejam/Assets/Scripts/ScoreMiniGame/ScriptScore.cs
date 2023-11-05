using System;
using TMPro;
using UnityEngine;

public class ScriptScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Click mainScene;
    [SerializeField] private int countScoreTabl = 0;
    void Start()
    {
    }
    public void AddScore()
    {
        countScoreTabl++;
        score.text = "Счёт: " + Convert.ToString(countScoreTabl);
        AudioManager.instance.PlaySfx("PointsGain");
        if (countScoreTabl == 3)
        {
            mainScene.gameObject.SetActive(true);
            mainScene.SwitchBackToGame();
            gameObject.SetActive(false);
        }
    }

    public void DecreaseScore()
    {
        countScoreTabl--;
        score.text = "Счёт: " + Convert.ToString(countScoreTabl);
        AudioManager.instance.PlaySfx("PointsLoss");
    }
}
