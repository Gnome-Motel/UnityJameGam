using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private FixedJoint2D joint;
    [SerializeField] private float speed;
    [SerializeField] private float launchSpeed = 25;
    [SerializeField] private float upSpeed = 25;
    [SerializeField] private float maxSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<FixedJoint2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude > maxSpeed) {
            
        }
        if (joint.enabled == true){
            float input = Input.GetAxisRaw("Horizontal");
            Vector2 movement = new Vector2(input * speed, 0);
            Debug.Log(movement);
            rb.velocity += transform.right * movement;
        }

    }

    void Update() {
        Debug.Log(rb.velocity.sqrMagnitude);
        if (Input.GetKeyDown(KeyCode.Space)) {
            joint.enabled = false;
            rb.velocity *= launchSpeed;
            rb.velocity += new Vector2(0, upSpeed);
        }
    }
}
