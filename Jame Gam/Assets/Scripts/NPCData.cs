using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public GameObject npcPrefab;
    [Range(0f, 1f)]
    public float spawnChance;
}
