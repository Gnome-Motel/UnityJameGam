using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSubmition : MonoBehaviour
{
    [Header("Score Display")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private string scoreTextDisplay = "Your Score: ";

    [Space]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button submitScoreButton;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreText.text = scoreTextDisplay + scoreManager.GetScore().ToString();
    }

    void Update()
    {
        
    }
}
