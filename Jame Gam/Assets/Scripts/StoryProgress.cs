using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryProgress : MonoBehaviour
{
    [SerializeField] List<Transform> startingPegs = new();
    [SerializeField] List<GameObject> levelSections = new();
    public static int level = 0;

    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] PlayerAttach playerAttach;

    public void UpdateLevel(Transform hookedPeg)
    {
        int pegLevel = levelSections.IndexOf(hookedPeg.transform.parent.gameObject);
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
            cameraFollow.riseSpeed = level *0.5f;
        }
    }

    public void GameOver()
    {
        Debug.Log("Ascended");
        playerAttach.Attatch(startingPegs[level]);
        cameraFollow.riseSpeed = 0f;
        cameraFollow.lowestY = startingPegs[level].transform.position.y;
    }
}
