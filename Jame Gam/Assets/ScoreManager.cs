using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;

    void Awake() {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int bonus) {
        score += bonus;
    }

    public int GetScore() {
        return score;
    }

    public void SetScore(int newScore) {
        score = newScore;
    }
}
