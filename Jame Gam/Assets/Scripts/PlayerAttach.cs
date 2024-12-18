using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttach : MonoBehaviour
{
    private CircleCollider2D coll;
    private bool near = false;
    [SerializeField] GameObject player;

    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            near = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            near = false;
        }
    }

    private void FixedUpdate()
    {
        if (near && Input.GetButtonDown("Grab"))
        {
            player.transform.parent = transform;
        }
        else if (near && Input.GetButtonUp("Grab"))
        {
            player.transform.parent = null;
        }
    }
}
