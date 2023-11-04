using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private int countScoreTabl = 0;
    void Start()
    {
    }
    public void AddScore()
    {
        countScoreTabl++;
        score.text = "Счёт: " + Convert.ToString(countScoreTabl);
    }

    public void DecreaseScore()
    {
        countScoreTabl--;
        score.text = "Счёт: " + Convert.ToString(countScoreTabl);
    }
}
