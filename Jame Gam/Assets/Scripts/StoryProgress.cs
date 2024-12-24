using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryProgress : MonoBehaviour
{
    [SerializeField] List<GameObject> startingPegs = new();
    [SerializeField] List<GameObject> levelSections = new();
    public int level = 0;

    public void UpdateLevel(Transform hookedPeg)
    {
        int pegLevel = levelSections.IndexOf(hookedPeg.transform.parent.gameObject);
        Debug.Log(pegLevel + hookedPeg.transform.parent.gameObject.name);
        if (level != pegLevel)
        {
            level = pegLevel;
            if (level > 1)
            {
                levelSections[level - 2].SetActive(false);
            }
            if (level < levelSections.Count - 1)
            {
                levelSections[level + 1].SetActive(true);
            }
        }
    }
}
