using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Dan.Main;

public class ScoreSubmition : MonoBehaviour
{

    // Public Key: 
    // Private Key: 0d580f1d6a1ee6122ec5abb2dcbf9c4a62cf7238b764c7af15e5b8b5ea00102572be03e0e1ce5cb901d15a209be46cffdfb591691dce7da6173d86b2d9c769b87cc62f5917497669d586f3427b0212eacce0afded8673b4bae454576b3807fc0b3d54297b919ec37f3865f1ee914b4126930b2139ef872088323a4aca577ce94
    [Header("Score Display")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private string scoreTextDisplay = "Your Score: ";

    [Space]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button submitScoreButton;
    private ScoreManager scoreManager;

    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;

    private string publicLeaderBoardKey = "d35c00dbf7255945795d3ad6a6e123b4e047aae14f0a09e168a3d368a236c29f";

    void Start()
    {
        GetLeaderboard();
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreText.text = scoreTextDisplay + scoreManager.GetScore().ToString();
    }

    public void SubmitScore() {
        int score = scoreManager.GetScore();
        string username = nameInputField.text;

        SetLeaderboardEntry(username, score);

        submitScoreButton.interactable = false;
        Destroy(scoreManager.gameObject);
    }

    public void GetLeaderboard() {
        LeaderboardCreator.GetLeaderboard(publicLeaderBoardKey, ((msg) => {
            for (int i = 0; i < names.Count; i++)
            {
                names[i].text = i + 1 + ". " + msg[i].Username + ":";
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score) {
        LeaderboardCreator.UploadNewEntry(publicLeaderBoardKey, username, score, ((_) => {
            GetLeaderboard();
        }));
    }


}
