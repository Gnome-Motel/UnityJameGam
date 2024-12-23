using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSection
{
    public float startY;
    public float endY;
    public LevelSectionConfig settings;
    public List<NPCData> NPCs = new List<NPCData>();
}
