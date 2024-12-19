using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{    
    [SerializeField] private int pegCount;

    [Header("Y Movement")]
    [SerializeField] private float minYStep;
    [SerializeField] private float maxYStep;

    [Header("X Movement")]
    [SerializeField] private float minXStep;
    [SerializeField] private float maxXStep;

    [Header("Scene Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startingPeg;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject peg;

    void Start()
    {
        
    }
}
