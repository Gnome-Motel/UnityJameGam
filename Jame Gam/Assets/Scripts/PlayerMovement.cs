using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float launchSpeed = 25;
    [SerializeField] private float upSpeed = 25;
    [SerializeField] private float maxSpeed = 1;

    //new variables for PlayerAttatch to contact
    public Transform lastPeg = null;
    public bool hooked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //temporary rotation demonstration
        if (hooked)
        {
            //transform.Rotate(0, 0, 7f);
        }
    }

    void Update() {
        //checks for 'Return' input
        if (Input.GetButtonDown("Return"))
        {
            Return(lastPeg);
        }
    }

    //function copied and modified from playerAttatch's Attatch
    //returns player to the last peg they were at
    //As of now, triggers on input down 'f', could be expanded to upon death as well.
    public void Return(Transform t)
    {
        if (lastPeg != null) 
        {
            rb.velocity = new Vector2(0f, 0f);
            transform.rotation = t.rotation;
            transform.position = t.position;
        }

    }
}
