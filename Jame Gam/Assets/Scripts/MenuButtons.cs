using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMOD.Studio;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject mode;

    [SerializeField] Button checkB;
    [SerializeField] Sprite checkYes;
    [SerializeField] Sprite checkNo;
    static bool hasPlayed = false;

    [SerializeField] GameObject camera;

    private EventInstance fireplaceS;
    private EventInstance pianoS;

    private void Start()
    {
        fireplaceS = AudioManager.instance.CreateEventInstance(FMODEvents.instance.fireplaceSound);
        pianoS = AudioManager.instance.CreateEventInstance(FMODEvents.instance.pianoSound);
        fireplaceS.start();
        pianoS.start();
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

    /*private void UpdateSound()
    {
        PLAYBACK_STATE stateP;
        PLAYBACK_STATE stateF;
        pianoS.getPlaybackState(out stateP);
        fireplaceS.getPlaybackState(out stateF);
        if (stateP.Equals(PLAYBACK_STATE.STOPPED))
        {
            pianoS.start();
        }
        if (stateF.Equals(PLAYBACK_STATE.STOPPED))
        {
            fireplaceS.start();
        }
    }*/
}
