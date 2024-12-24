using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Fireplace sounds")]
    [field: SerializeField] public EventReference fireplaceSound { get; private set; }

    [field: Header("Piano sounds")]
    [field: SerializeField] public EventReference pianoSound { get; private set; }

    [field: Header("Wind sounds")]
    [field: SerializeField] public EventReference windSound { get; private set; }

    [field: Header("Peg sounds")]
    [field: SerializeField] public EventReference pegSound { get; private set; }

    [field: Header("Fall sounds")]
    [field: SerializeField] public EventReference fallSound { get; private set; }

    [field: Header("Gameover sounds")]
    [field: SerializeField] public EventReference gameoverSound { get; private set; }

    [field: Header("Level music")]
    [field: SerializeField] public EventReference PlayingSound { get; private set; }

    [field: Header("Ocarina happy sounds")]
    [field: SerializeField] public EventReference OcaHSound { get; private set; }

    [field: Header("Ocarina sad sounds")]
    [field: SerializeField] public EventReference OcaSSound { get; private set; }

    [field: Header("Kalimba happy sounds")]
    [field: SerializeField] public EventReference KalHSound { get; private set; }

    [field: Header("Kalimba sad sounds")]
    [field: SerializeField] public EventReference KalSSound { get; private set; }

    [field: Header("Steel Drums happy sounds")]
    [field: SerializeField] public EventReference DrumHSound { get; private set; }

    [field: Header("Steel Drums sad sounds")]
    [field: SerializeField] public EventReference DrumSSound { get; private set; }

    [field: Header("Trumpet happy sounds")]
    [field: SerializeField] public EventReference TruHSound { get; private set; }

    [field: Header("Trumpet sad sounds")]
    [field: SerializeField] public EventReference TruSSound { get; private set; }

    [field: Header("Strings happy sounds")]
    [field: SerializeField] public EventReference StrHSound { get; private set; }

    [field: Header("Strings sad sounds")]
    [field: SerializeField] public EventReference StrSSound { get; private set; }

    [field: Header("Violin happy sounds")]
    [field: SerializeField] public EventReference VioHSound { get; private set; }

    [field: Header("Violin sad sounds")]
    [field: SerializeField] public EventReference VioSSound { get; private set; }

    [field: Header("Love Song")]
    [field: SerializeField] public EventReference loveSound { get; private set; }

    public static FMODEvents instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one FMODEvents in current scene!");
        }
        instance = this;
    }


}
