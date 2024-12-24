using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    void Start() {
        canvas.SetActive(false);
    }

    public void DisplayScreen() {
        canvas.SetActive(true);
    }

    public void HideScreen() {
        canvas.SetActive(false);
    }

    public void Respawn() {
        HideScreen();
        FindObjectOfType<StoryProgress>().GameOver();
    }
}
