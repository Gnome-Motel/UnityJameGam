using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private Transform startingPeg;

    //new variables for PlayerAttatch to contact
    [HideInInspector] public Transform lastPeg = null;
    [HideInInspector] public bool hooked = false;

    public int maxLives;
    [HideInInspector] public int lives;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lives = maxLives;
        Return(startingPeg);
        lastPeg = startingPeg;
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

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Respawn")){
            Return(lastPeg);
            anim.SetTrigger("respawn");
        }
    }

    //function copied and modified from playerAttatch's Attatch
    //returns player to the last peg they were at
    //As of now, triggers on input down 'f', could be expanded to upon death as well.
    public void Return(Transform peg)
    {
        if (peg != null) 
        {
            peg.GetComponent<PlayerAttach>().Attatch(peg.transform);
            rb.velocity = new Vector2(0f, 0f);
            transform.rotation = peg.rotation;
            transform.position = peg.position;
        }

    }
}
