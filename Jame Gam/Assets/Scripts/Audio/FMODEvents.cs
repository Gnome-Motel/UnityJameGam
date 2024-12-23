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
