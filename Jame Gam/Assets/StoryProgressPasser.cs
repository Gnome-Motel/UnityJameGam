using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryProgressPasser : MonoBehaviour
{
    public float timer;
    public int gifts;

    PlayerMovement player;

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        player = FindObjectOfType<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        gifts = player.presentsDelivered;
    }
}
