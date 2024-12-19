using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    //new variables for PlayerAttatch to contact
    public Transform lastPeg = null;
    public bool hooked = false;

    public int maxLives;
    public int lives;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lives = maxLives;
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
            lives -= 1;
            if (lives <= 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            GetComponent<PlayerAttach>().Attatch(peg.transform, false);
            rb.velocity = new Vector2(0f, 0f);
            transform.rotation = peg.rotation;
            transform.position = peg.position;
        }

    }
}
