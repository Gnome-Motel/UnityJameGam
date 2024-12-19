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


    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            near = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("out");
            near = false;
        }
    }

    private void Update()
    {   
        if (Input.GetButtonDown("Grab")){
            if (near)
            {
                if (player.transform.parent == null)
                {
                    Attatch(transform);
                    return;
                }
                if (playerMovement.hooked)
                {
                    player.transform.parent = null;
                    playerRB.constraints = RigidbodyConstraints2D.None;
                    playerMovement.hooked = false;
                    Debug.Log(currentGravity);
                    playerRB.velocity = player.transform.right * currentGravity / 25;
                }
            }
        }

        float movement = Input.GetAxisRaw("Horizontal");
        currentGravity += movement * moveSpeed * Time.deltaTime;

        if (playerMovement.hooked) {
            AddGravityToPlayer(player.transform);
        }

        if (playerRB != null){
            Debug.Log(playerRB.velocity.sqrMagnitude);
        }
    }

    void AddGravityToPlayer(Transform player) {
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
}
