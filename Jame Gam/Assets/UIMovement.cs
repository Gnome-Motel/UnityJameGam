using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour
{
    [SerializeField] private GameObject child;

    void LateUpdate() {
        float xDiff = transform.position.x % 0.0625f;
        float yDiff = transform.position.y % 0.0625f;
        float zDiff = transform.position.z % 0.0625f;

        child.transform.position = new Vector3(transform.position.x -xDiff, transform.position.y - yDiff, transform.position.z - zDiff);
    }
}
