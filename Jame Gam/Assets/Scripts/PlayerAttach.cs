using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttach : MonoBehaviour
{
    private bool near = false;
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] PlayerMovement mover;

    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

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
        if (near && Input.GetButton("Grab"))
        {
            if (player.transform.parent == null)
            {
                Attatch(transform);
            }
        }
        else
        {
            player.transform.parent = null;
            playerRB.constraints = RigidbodyConstraints2D.None;
            mover.hooked = false;
        }
    }

    //Function to hook a player to a peg. Locks individual movement, parents them, aligns player, and updates the most recent peg
    public void Attatch(Transform t)
    {
        player.transform.parent = t;
        player.transform.position = t.position;
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        mover.lastPeg = t;
        mover.hooked = true;
    }
}
