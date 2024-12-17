using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(input * speed, 0);
        Debug.Log(movement);
        rb.velocity += transform.right * movement;
    }
}
