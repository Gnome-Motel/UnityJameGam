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
        FindObjectOfType<PlayerMovement>().paused = true;
    }

    public void HideScreen() {
        canvas.SetActive(false);
    }

    public void Respawn() {
        HideScreen();
        FindObjectOfType<PlayerMovement>().paused = false;
        FindObjectOfType<StoryProgress>().GameOver();
    }
}
