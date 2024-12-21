using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWrap : MonoBehaviour
{
    [SerializeField] private float xLimit;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.x < -xLimit) {
            //rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x), rb.velocity.y);
            transform.position = new Vector2(xLimit, transform.position.y);
        }

        if (transform.position.x > xLimit) {
            //rb.velocity = new Vector2(-Mathf.Abs(rb.velocity.x), rb.velocity.y);

            transform.position = new Vector2(-xLimit, transform.position.y);
        }
    }
}
