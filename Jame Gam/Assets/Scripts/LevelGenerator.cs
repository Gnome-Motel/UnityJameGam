using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Level Sections")]
    [SerializeField] LevelSection[] levels;
    private LevelSectionConfig currentLevel;

    [Header("X Movement")]
    [SerializeField] private float xRange;

    [Header("New Level Generation")]
    [SerializeField] private int pegCount;
    [SerializeField] private float yBuffer;

    [Header("Scene Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject startingPeg;

    [Header("Prefabs")]
    [SerializeField] private GameObject peg;

    void Start()
    {
        currentLevel = levels[0].settings;
        GenerateLevel(transform.position.y, pegCount, xRange, currentLevel.minimumPegDistance, currentLevel.doublePegRate, currentLevel.triplePegRate, currentLevel.quadPegRate, currentLevel.minYStep, currentLevel.maxYStep);
    }

    void Update()
    {
        if (player.transform.position.y > transform.position.y - yBuffer) {
            for (int i = 0; i < levels.Length; i++)
            {
                if (transform.position.y < levels[i].startY) {
                    continue;
                }
                
                if (transform.position.y >= levels[i].endY){
                    continue;
                }

                currentLevel = levels[i].settings;
                break;
            }
            GenerateLevel(transform.position.y, pegCount, xRange, currentLevel.minimumPegDistance, currentLevel.doublePegRate, currentLevel.triplePegRate, currentLevel.quadPegRate, currentLevel.minYStep, currentLevel.maxYStep);
        }
    }

    void GenerateLevel(float _startY, int _layerCount, float _layerSpread, float _minimumPegDistance, float _doublePegRate, float _triplePegRate, float _quadPegRate, float _minYStep, float _maxYStep)
    {
        float currentY = _startY;
        int i = 0;
        while (i < _layerCount)
        {
            List<Transform> pegs = new List<Transform>();


            pegs.Add(PlacePeg(peg, pegs, _minimumPegDistance, _layerSpread, currentY).transform);

            if (Random.Range(0f, 1f) < _doublePegRate)
            {
                GameObject doublePeg = PlacePeg(peg, pegs, _minimumPegDistance, _layerSpread, currentY);
                if (doublePeg != null)
                {
                    pegs.Add(doublePeg.transform);
                }
                if (Random.Range(0f, 1f) < _triplePegRate)
                {
                    GameObject triplePeg = PlacePeg(peg, pegs, _minimumPegDistance, _layerSpread, currentY);
                    if (triplePeg != null)
                    {
                        pegs.Add(triplePeg.transform);
                    }
                    if (Random.Range(0f, 1f) < _quadPegRate)
                    {
                        GameObject quadPeg = PlacePeg(peg, pegs, _minimumPegDistance, _layerSpread, currentY);
                        if (quadPeg != null)
                        {
                            pegs.Add(quadPeg.transform);
                        }
                    }
                }
            }
            currentY += Random.Range(_minYStep, _maxYStep);
            i += 1;
        }
        transform.position = new Vector2(0, currentY);
    }

    GameObject PlacePeg(
        GameObject pegPrefab,
        List<Transform> pegs,
        float minimumDistanceFromOtherPegs,
        float screenWidthInUnityUnits,
        float currentYPosition
        )
    {

        float x = Random.Range(-screenWidthInUnityUnits, screenWidthInUnityUnits);
        Vector2 position = new Vector2(x, currentYPosition);

        int attempts = 3;
        if (pegs.Count > 0)
        {
            while (NearOtherPegs(pegs, position, minimumDistanceFromOtherPegs) && attempts > 0)
            {
                x = Random.Range(-screenWidthInUnityUnits, screenWidthInUnityUnits);
                position = new Vector2(x, currentYPosition);
                attempts -= 1;
            }
            if (attempts == 0)
            {
                return null;
            }
        }

        GameObject newPeg = Instantiate(pegPrefab, position, Quaternion.identity);
        return newPeg;
    }

    bool NearOtherPegs(List<Transform> pegs, Vector2 currentPegPosition, float minimumDistance)
    {
        bool result = false;

        for (int i = 0; i < pegs.Count; i++)
        {
            float pegDistance = Vector2.Distance(pegs[i].position, currentPegPosition);
            if (pegDistance >= minimumDistance)
            {
                continue;
            }

            result = true;
        }

        return result;
    }
}
