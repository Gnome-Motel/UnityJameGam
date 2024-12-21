using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using System;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    private float lowestY;
    private PlayerMovement playerMovement;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
    private int displayScore;
    [SerializeField] private float followSpeed;
    [SerializeField] private float riseSpeed = 0.125f;
    [SerializeField] private float riseSpeedIncreaseRate = 8;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = target.GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (playerMovement.hooked && lowestY < transform.position.y) {
            lowestY = transform.position.y;
            score = (int)MathF.Round(lowestY*100, 0);
        }
        if (playerMovement.hooked) { 
            lowestY += Time.deltaTime * riseSpeed;
            riseSpeed = Mathf.Sqrt(score) / riseSpeedIncreaseRate;
        }

        if (displayScore + 25 <= score ) {
            displayScore +=5;
        } else if (displayScore < score) {
            displayScore+=1;
        }

        if (displayScore > 0 ){
            scoreText.text = displayScore.ToString();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float y = lowestY;
        if (target.position.y > lowestY) {
            y = target.position.y;
        }

        Vector3 pos;
        if (playerMovement.hooked) {
            pos = new Vector3(offset.x, Mathf.Lerp(transform.position.y, y, followSpeed), target.position.z + offset.z);
        } else {
            pos = new Vector3(offset.x, y, target.position.z + offset.z);
        }   
        transform.position = pos;
    }
}
