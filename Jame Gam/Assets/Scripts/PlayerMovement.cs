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
    [HideInInspector] public Transform lastPeg = null;
    [HideInInspector] public bool hooked = false;

    public int maxLives;
    [HideInInspector] public int lives;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lives = maxLives;
    }

    void OnTriggerStay2D(Collider2D other) {
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
                FindObjectOfType<SceneTransition>().ReloadScene();
            }
            FindObjectOfType<CameraFollow>().gameObject.GetComponent<Animator>().SetTrigger("shake");
            PlayerAttach attach = GetComponent<PlayerAttach>();
            attach.Attatch(peg.transform, false);
            attach.currentGravity = 0;
            rb.velocity = new Vector2(0f, 0f);
            if (Random.Range(0f, 1f) > 0.5){
                transform.rotation = Quaternion.Euler(0, 0, 45);
            } else {
                transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            transform.position = peg.position;
        }

    }
}
