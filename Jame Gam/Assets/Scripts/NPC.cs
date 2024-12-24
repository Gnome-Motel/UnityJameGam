using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private float currentGravity;
    [SerializeField] private float gravitySpeed = 800;
    public bool collected = false;

    void Update()
    {
        SwingSideToSide();
    }

    public virtual void Collect() {

    }

    void SwingSideToSide() {
        if (transform.rotation.z > 0) {
            currentGravity -= gravitySpeed * Time.deltaTime;
        }

        if (transform.rotation.z < 0) {
            currentGravity += gravitySpeed * Time.deltaTime;
        }

        transform.Rotate(new Vector3(0, 0, currentGravity * Time.deltaTime));
    }
}
