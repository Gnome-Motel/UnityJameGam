using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrap : MonoBehaviour
{
    [SerializeField] private float xLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -xLimit) {
            transform.position = new Vector2(xLimit, transform.position.y);
        }

        if (transform.position.x > xLimit) {
            transform.position = new Vector2(-xLimit, transform.position.y);
        }
    }
}
