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
    [SerializeField] private float pegDistance = 4f;

    [Header("Scene Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startingPeg;

    [Header("Prefabs")]
    [SerializeField] private GameObject peg;

    void Start()
    {
        float currentY = transform.position.y;
        int i = 0;
        while (i < pegCount){
            float previouxX;
            Vector2 position;
            position = new Vector2(Random.Range(-xRange, xRange), currentY);
            Instantiate(peg, position, Quaternion.identity);
            previouxX = position.x;

            if (Random.Range(0f, 1f) < doublePegRate) {
                do {
                    position = new Vector2(Random.Range(-xRange, xRange), currentY);
                } while (Vector2.Distance(position, new Vector2(previouxX, currentY)) < pegDistance);
                Instantiate(peg, position, Quaternion.identity);
            }
            currentY += Random.Range(minYStep, maxYStep);
            i += 1;
        }
    }
}
