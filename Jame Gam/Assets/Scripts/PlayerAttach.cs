using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttach : MonoBehaviour
{
    private GameObject nearestPeg = null;
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

    [SerializeField] private Transform startingPeg;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        trajectoryLine = GetComponent<LineRenderer>();

        Attatch(startingPeg);
        playerMovement.lastPeg = startingPeg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Peg"))
        {
            nearestPeg = other.gameObject;
            nearestPeg.GetComponent<Animator>().SetBool("near", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Peg"))
        {
            nearestPeg.GetComponent<Animator>().SetBool("near", false);
            nearestPeg = null;
        }
    }

    private void Update()
    {   
        grabTimeLeft -= Time.deltaTime;
        if (Input.GetButtonDown("Grab")){
            grabTimeLeft = grabTimeBuffer;
        }

        if (grabTimeLeft > 0 && nearestPeg != null){
            if (transform.parent == null)
            {
                    Attatch(nearestPeg.transform);
                    grabTimeLeft = 0;
                    return;
            }
            if (playerMovement.hooked)
            {
                LaunchPlayer();
                grabTimeLeft = 0;
                return;
            }

        }

        if (playerMovement.hooked) {
            AddGravityToPlayer(transform);
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

    void LaunchPlayer(){
        transform.parent = null;
        playerRB.constraints = RigidbodyConstraints2D.None;
        playerMovement.hooked = false;
        playerRB.velocity = transform.right * currentGravity / 20;
        grabTimeLeft = 0;
    }

    //Function to hook a player to a peg. Locks individual movement, parents them, aligns player, and updates the most recent peg
    public void Attatch(Transform peg, bool resetLives = true)
    {
        gravitySpeed = initialGravitySpeed;
        if (transform.rotation.z < 0) {
            currentGravity = initialGravitySpeed /2;
        }
        if (transform.rotation.z > 0) {
            currentGravity = -initialGravitySpeed / 2;
        }

        transform.parent = peg;
        transform.position = peg.position;
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        if (resetLives) {
            playerMovement.lives = playerMovement.maxLives;
        }

        playerMovement.lastPeg = peg;
        playerMovement.hooked = true;
    }

    void DrawTrajectory(){
        if (nearestPeg == null || transform.parent == null)
        {
            trajectoryLine.positionCount = 0;
            return;
        }

        Vector2 startPosition = transform.position;
        Vector2 startVelocity = transform.right * currentGravity / 20;
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
