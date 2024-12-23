using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    //new variables for PlayerAttatch to contact
    [HideInInspector] public Transform lastPeg = null;
    [HideInInspector] public Transform highestPeg;
    [HideInInspector] public bool hooked = false;

    EventInstance fallS;
    EventInstance gameoverS;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fallS = AudioManager.instance.CreateEventInstance(FMODEvents.instance.fallSound);
        gameoverS = AudioManager.instance.CreateEventInstance(FMODEvents.instance.gameoverSound);
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Respawn")){
            Return(highestPeg);
            anim.SetTrigger("respawn");
        }
    }

    public void Return(Transform peg)
    {

        if (peg != null) 
        {   
            GameObject camera = FindObjectOfType<CameraFollow>().gameObject;
            if (highestPeg.transform.position.y <= camera.GetComponent<CameraFollow>().deathPlane.position.y) {
                gameoverS.start();
                FindObjectOfType<SceneTransition>().LoadScene("DeathScreen");
            }
            else
            {
                fallS.start();
            }
            camera.GetComponent<Animator>().SetTrigger("shake");
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
