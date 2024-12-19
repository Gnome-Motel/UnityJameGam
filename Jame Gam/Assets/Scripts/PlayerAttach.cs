using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttach : MonoBehaviour
{
    private bool near = false;
    public GameObject player;
    Rigidbody2D playerRB;
    PlayerMovement playerMovement;

    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

    [SerializeField] float initialGravitySpeed;
    [SerializeField] float damping;
    [SerializeField] float moveSpeed;
    float gravitySpeed;
    [HideInInspector] public float currentGravity;

    [SerializeField] LineRenderer trajectoryLine;
    [SerializeField] int trajectoryResolution = 30;
    [SerializeField] float trajectoryTimeStep = 0.1f;

    [SerializeField] float grabTimeBuffer = 0.3f;
    float grabTimeLeft;

    private Animator anim;


    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<PlayerMovement>();
        trajectoryLine = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            near = true;
            anim.SetBool("near", near);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            near = false;
            anim.SetBool("near", near);
        }
    }

    private void Update()
    {   
        grabTimeLeft -= Time.deltaTime;
        if (Input.GetButtonDown("Grab")){
            grabTimeLeft = grabTimeBuffer;
        }

        if (grabTimeLeft > 0){
            if (near)
            {
                if (player.transform.parent == null)
                {
                    Attatch(transform);
                    grabTimeLeft = 0;
                    return;
                }
                if (playerMovement.hooked)
                {
                    player.transform.parent = null;
                    playerRB.constraints = RigidbodyConstraints2D.None;
                    playerMovement.hooked = false;
                    Debug.Log(currentGravity);
                    playerRB.velocity = player.transform.right * currentGravity / 20;
                    grabTimeLeft = 0;

                }
            }
        }

        if (playerMovement.hooked && near) {
            AddGravityToPlayer(player.transform);
        }

        if (playerRB != null){
            Debug.Log(playerRB.velocity.sqrMagnitude);
        }

        DrawTrajectory();
    }

    void AddGravityToPlayer(Transform player) {
        float movement = Input.GetAxisRaw("Horizontal");
        currentGravity += movement * moveSpeed * Time.deltaTime;

        if (player.rotation.z > 0) {
            currentGravity -= gravitySpeed * Time.deltaTime;
        }

        if (player.rotation.z < 0) {
            currentGravity += gravitySpeed * Time.deltaTime;
        }

        currentGravity *= 1f - damping * Time.deltaTime;

        player.Rotate(new Vector3(0, 0, currentGravity * Time.deltaTime));

    }

    //Function to hook a player to a peg. Locks individual movement, parents them, aligns player, and updates the most recent peg
    public void Attatch(Transform t)
    {
        gravitySpeed = initialGravitySpeed;
        if (player.transform.rotation.z < 0) {
            currentGravity = initialGravitySpeed /2;
        }
        if (player.transform.rotation.z > 0) {
            currentGravity = -initialGravitySpeed / 2;
        }
        player.transform.parent = t;
        player.transform.position = t.position;
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        playerMovement.lastPeg = t;
        playerMovement.hooked = true;
    }

    void DrawTrajectory(){
        if (!near || player.transform.parent == null)
        {
            trajectoryLine.positionCount = 0;
            return;
        }

        Vector2 startPosition = player.transform.position;
        Vector2 startVelocity = player.transform.right * currentGravity / 20;
        Vector2 gravity = Physics2D.gravity;

        trajectoryLine.positionCount = trajectoryResolution;

        for (int i = 0; i < trajectoryResolution; i++)
        {
            float time = i * trajectoryTimeStep;
            Vector2 position = startPosition + startVelocity * time + 0.5f * gravity * time * time;
            trajectoryLine.SetPosition(i, position);
        }
    }

}
