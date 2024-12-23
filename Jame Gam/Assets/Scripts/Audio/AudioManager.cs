using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private List<EventInstance> eventInstances;

    public EventInstance windInstance;

    [Header("Volume")]
    [Range(0,1)]
    public float musicVolume = 1f;
    [Range(0, 1)]
    public float SFXVolume = 1f;

    private Bus musicBus;
    private Bus SFXBus;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null)
        {
            Debug.Log("More than one AudioManager in current scene!");
        }
        instance = this;

        eventInstances = new List<EventInstance>();

        musicBus = RuntimeManager.GetBus("bus:/Music");
        SFXBus = RuntimeManager.GetBus("bus:/SFX");
    }

    public void OneShot(EventReference sound, Vector3 pos)
    {
        RuntimeManager.PlayOneShot(sound, pos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void AudioEnd()
    {
        foreach (EventInstance e in eventInstances)
        {
            e.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            e.release();
        }
    }
    private void OnDestroy()
    {
        AudioEnd();
    }

    public void InitializeWind(EventReference windReference)
    {
        windInstance = RuntimeManager.CreateInstance(windReference);
        windInstance.start();
    }

    public void SetWindVolume(string parameter, float value)
    {
        windInstance.setParameterByName(parameter, value);
    }

    public void MusicUpdate(float volume)
    {
        musicBus.setVolume(volume/10f);
    }

    public void SFXUpdate(float volume)
    {
        SFXBus.setVolume(volume/10f);
    }
}
