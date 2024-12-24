using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentCollect : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Collider2D coll;
    private Rigidbody2D rb;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("touched");
        if (other.CompareTag("Player"))
        {
            Debug.Log("by player");
            playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.presentCount++;
            Destroy(this.gameObject);
        }
    }
}
