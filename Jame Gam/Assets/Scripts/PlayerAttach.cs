using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using FMOD.Studio;

public class PlayerAttach : MonoBehaviour
{
    private GameObject nearestPeg = null;
    private Rigidbody2D playerRB;
    private PlayerMovement playerMovement;

    [SerializeField] private Transform startingPeg;
    [SerializeField] private bool story;
    [SerializeField] public StoryProgress prog;

    [Header("Hooked Movement")]
    [SerializeField] float initialGravitySpeed;
    [SerializeField] float damping;
    [SerializeField] float moveSpeed;
    float gravitySpeed;
    [HideInInspector] public float currentGravity;

    [Header("Trajectory")]
    [SerializeField] int trajectoryResolution = 30;
    [SerializeField] float trajectoryTimeStep = 0.1f;
    LineRenderer trajectoryLine;

    [Header("Player Help")]
    [SerializeField] float grabTimeBuffer = 0.3f;
    float grabTimeLeft;

    [Header("Effects")]
    [SerializeField] private GameObject grabEffect;


    private EventInstance clickS;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        trajectoryLine = GetComponent<LineRenderer>();
        gravitySpeed = initialGravitySpeed;

        playerMovement.highestPeg = startingPeg;
        Attatch(startingPeg);
        playerMovement.lastPeg = startingPeg;

        AudioManager.instance.InitializeWind(FMODEvents.instance.windSound);
        clickS = AudioManager.instance.CreateEventInstance(FMODEvents.instance.pegSound);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("NPC")) {
            NPC npc = other.GetComponent<NPC>();
            if (npc.collected) {
                return;
            }
            npc.Collect();
            Instantiate(grabEffect, npc.gameObject.transform.position, quaternion.identity);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Peg"))
        {
            nearestPeg = other.gameObject;
            other.GetComponent<Animator>().SetBool("near", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Peg"))
        {
            other.GetComponent<Animator>().SetBool("near", false);
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
                PegSound();
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

        //DrawTrajectory();
        AudioManager.instance.SetWindVolume("Wind intensity", transform.position.y/100f);
    }

    void AddGravityToPlayer(Transform player) {
        float movement = Input.GetAxisRaw("Horizontal");
        currentGravity += movement * moveSpeed * Time.deltaTime;
        if (Mathf.Abs(currentGravity) <= 5) {
            currentGravity += movement * moveSpeed * 10 * Time.deltaTime;
        }

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
        playerRB.velocity = transform.right * (Mathf.Sqrt(Mathf.Abs(currentGravity+0.0001f))/2) * (currentGravity/Mathf.Abs(currentGravity));
        grabTimeLeft = 0;
    }

    //Function to hook a player to a peg. Locks individual movement, parents them, aligns player, and updates the most recent peg
    public void Attatch(Transform peg, bool resetLives = true)
    {
        if (story)
        {
            prog.UpdateLevel(peg);
        }
        float xMagnitude = playerRB.velocity.x * (1- (transform.rotation.z / 90));
        float yMagnitude = playerRB.velocity.y * (transform.rotation.z / 90);

        currentGravity = 2 * ((playerRB.velocity.x * xMagnitude) + (playerRB.velocity.y * yMagnitude));

        transform.parent = peg;
        transform.position = peg.position;
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        if (resetLives) {
            if (peg != playerMovement.lastPeg) {
                Instantiate(grabEffect, transform.position, quaternion.identity);
            }
        }
        

        playerMovement.lastPeg = peg;
        playerMovement.hooked = true;

        if (peg.position.y >= playerMovement.highestPeg.position.y) {
            playerMovement.highestPeg = peg;
        }
    }

    void DrawTrajectory(){
        if (nearestPeg == null || transform.parent == null)
        {
            trajectoryLine.positionCount = 0;
            return;
        }

        Vector2 startPosition = transform.position;
        Vector2 startVelocity = transform.right * (Mathf.Sqrt(Mathf.Abs(currentGravity+0.0001f))/2) * (currentGravity/Mathf.Abs(currentGravity));
        Vector2 gravity = Physics2D.gravity;

        trajectoryLine.positionCount = trajectoryResolution;

        for (int i = 0; i < trajectoryResolution; i++)
        {
            float time = i * trajectoryTimeStep;
            Vector2 position = startPosition + startVelocity * time + 0.5f * gravity * time * time;
            trajectoryLine.SetPosition(i, position);
        }
    }

    void PegSound()
    {
        clickS.setParameterByName("Grab Speed", playerRB.velocity.magnitude/10f);
        clickS.start();
    }
}
