using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttach : MonoBehaviour
{
    private bool near = false;
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

    [SerializeField] float initialGravitySpeed;
    [SerializeField] float damping;
    float gravitySpeed;
    float currentGravity;


    void Start()
    {
        //coll = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Didn't Worked");
        if (collision.gameObject == player)
        {
            near = true;
            Debug.Log("Worked");
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
            }

            if (playerMovement.hooked)
            {
                player.transform.parent = null;
                playerRB.constraints = RigidbodyConstraints2D.None;
                playerMovement.hooked = false;
            }
        }

        if (playerMovement.hooked) {
            AddGravityToPlayer(player.transform);
        }

    }

    void AddGravityToPlayer(Transform player) {
        if (gravitySpeed > -10) {
            gravitySpeed -= damping * Time.deltaTime;
        }

        if (player.rotation.z > 10) {
            currentGravity -= gravitySpeed * Time.deltaTime;
        }

        if (player.rotation.z < 0) {
            currentGravity += gravitySpeed * Time.deltaTime;
        }

        player.Rotate(new Vector3(0, 0, currentGravity * Time.deltaTime));
        currentGravity *= 0.99f;

    }

    //Function to hook a player to a peg. Locks individual movement, parents them, aligns player, and updates the most recent peg
    public void Attatch(Transform t)
    {
        gravitySpeed = initialGravitySpeed;
        player.transform.parent = t;
        player.transform.position = t.position;
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        playerMovement.lastPeg = t;
        playerMovement.hooked = true;
    }
}
