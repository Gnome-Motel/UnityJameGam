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
    [SerializeField] private float xRange;
    [SerializeField] private float doublePegRate = 0.2f;

    [Header("Scene Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startingPeg;

    [Header("Prefabs")]
    [SerializeField] private GameObject peg;

    private float currentY;

    void Start()
    {
        int i = 0;
        while (i < pegCount){
            do {
                Vector2 position = new Vector2(Random.Range(-xRange, xRange), currentY);
                GameObject currentPeg = Instantiate(peg, position, Quaternion.identity);
                currentPeg.GetComponent<PlayerAttach>().player = player;
            } while (Random.Range(0f, 1f) < doublePegRate);
            currentY += Random.Range(minYStep, maxYStep);
            i += 1;
        }
    }
}
