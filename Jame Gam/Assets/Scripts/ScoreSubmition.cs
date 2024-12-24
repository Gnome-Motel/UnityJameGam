using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Dan.Main;

public class ScoreSubmition : MonoBehaviour
{

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

    public void ResetScore() {
        Destroy(gameObject);
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
