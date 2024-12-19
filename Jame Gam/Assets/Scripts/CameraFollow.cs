using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    private float lowestY;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = target.GetComponent<PlayerMovement>();
    }

    void Update() {
        if (playerMovement.hooked) {
            lowestY = transform.position.y;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float y = lowestY;
        if (target.position.y > lowestY) {
            y = target.position.y;
        }

        Vector3 pos = new Vector3(transform.position.x, y, target.position.z + offset.z);
        transform.position = pos;
    }
}
