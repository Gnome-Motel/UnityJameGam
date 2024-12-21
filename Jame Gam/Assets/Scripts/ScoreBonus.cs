using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBonus : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Colours")]
    [SerializeField] private Color positiveColor;
    [SerializeField] private Color negativeColor;

    public void ConfigureScoreSettings(int score){
        bool positive = true;
        if (score / Mathf.Abs(score) < 0) {
            positive = false;
        }

        if (positive) {
            scoreText.color = positiveColor;
            scoreText.text = "+" + score;
        } else {
            scoreText.color = positiveColor;
            scoreText.text = score.ToString();
        }
    }
}
