using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC_Interaction : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private SpriteRenderer spr;
    private Rigidbody2D rb;
    private Collider2D coll;

    [SerializeField] string happyText;
    [SerializeField] string unhappyText;
    private bool hasSaidSad = false;
    private bool hasSaidHappy = false;

    [SerializeField] Sprite happySprite;

    [SerializeField] private TextMeshProUGUI textDisplay;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("touched");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("by player");
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement.presentCount > 0 && !hasSaidHappy)
            {
                Debug.Log("congrats");
                playerMovement.presentCount--;
                playerMovement.presentsDelivered++;
                spr.sprite = happySprite;
                textDisplay.text = happyText;
                hasSaidHappy=true;
            }
            else if (!hasSaidSad)
            {
                Debug.Log("Sadness");
                hasSaidSad=true;
                textDisplay.text = unhappyText;
            }
        }
    }
}
