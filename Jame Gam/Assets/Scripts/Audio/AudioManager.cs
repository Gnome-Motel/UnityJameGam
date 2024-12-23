using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private List<EventInstance> eventInstances;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one AudioManager in current scene!");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
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
}
