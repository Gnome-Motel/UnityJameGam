using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryProgress : MonoBehaviour
{
    public Transform[] startingPegs;
    [SerializeField] GameObject[] levelSections;
    public static int level = 0;

    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] PlayerAttach playerAttach;

    public void UpdateLevel(Transform hookedPeg)
    {
        ValidateStartingPegs();

        int pegLevel = System.Array.IndexOf(levelSections, hookedPeg.transform.parent.gameObject);
        if (level != pegLevel)
        {
            level = pegLevel;
            if (level > 1)
            {
                levelSections[level - 2].SetActive(false);
            }
            if (level < levelSections.Length - 1)
            {
                levelSections[level + 1].SetActive(true);
            }
            cameraFollow.riseSpeed = level *0.3f;
        }
    }

    public void GameOver()
    {
        Transform startingPeg = startingPegs[level];
        Debug.Log(startingPeg);
    
        float yPos = startingPeg.position.y;
        cameraFollow.gameObject.transform.position = new Vector2(cameraFollow.gameObject.transform.position.x, yPos);
        cameraFollow.lowestY = yPos;

        playerAttach.Attatch(startingPeg, resetLevel:false);
        Debug.Log("Attached to starting peg");

        cameraFollow.riseSpeed = 0f;
        Debug.Log("Reset Camera");


        Debug.Log("Ascended");
    }

    private void LogStartingPegs()
    {
        Debug.Log($"StartingPegs count: {startingPegs.Length}");
        for (int i = 0; i < startingPegs.Length; i++)
        {
            Debug.Log($"startingPegs[{i}]: {startingPegs[i]?.name ?? "null"}");
        }
    }

    private void ValidateStartingPegs()
    {
    for (int i = 0; i < startingPegs.Length; i++)
    {
        if (startingPegs[i] == null)
        {
            Debug.LogError($"startingPegs[{i}] is missing!", this);
        }
    }
    }
}
