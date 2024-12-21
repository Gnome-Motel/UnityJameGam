using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Section", menuName = "Level")]
public class LevelSectionConfig : ScriptableObject
{
    [Header("Vertical Movement")]
    public float minYStep;
    public float maxYStep;

    [Header("Peg Placement")]
    public float minimumPegDistance;
    [Range(0f, 1f)]
    public float doublePegRate;
    [Range(0f, 1f)]
    public float triplePegRate;
    [Range(0f, 1f)]
    public float quadPegRate;

}
