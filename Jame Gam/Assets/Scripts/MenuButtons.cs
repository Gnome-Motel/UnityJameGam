using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject mode;

    [SerializeField] Button checkB;
    [SerializeField] Sprite checkYes;
    [SerializeField] Sprite checkNo;
    static bool hasPlayed = false;

    static float musicVolume;
    static float soundVolume;
    [SerializeField] GameObject camera;
    AudioSource[] audios;

    private void Start()
    {
        audios = camera.GetComponents<AudioSource>();
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void StoryMode()
    {
        if (hasPlayed)
        {
            SceneManager.LoadScene("Story Mode");
        }
        else
        {
            SceneManager.LoadScene("Opening");
        }
    }
    public void Endless()
    {
        SceneManager.LoadScene("Arcade");
    }

    public void playHistoryToggle()
    {
        if (hasPlayed)
        {
            hasPlayed = false;
            checkB.image.sprite = checkNo;
        }
        else 
        {
            hasPlayed = true;
            checkB.image.sprite = checkYes;
        }
    }

    public void MusicVolume(float vol)
    {
        musicVolume = vol / 10f;
        audios[0].volume = musicVolume;
    }

    public void SoundVolume(float vol)
    {
        soundVolume = vol / 10f;
        audios[1].volume = soundVolume;
    }
}
