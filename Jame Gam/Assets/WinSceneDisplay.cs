using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinSceneDisplay : MonoBehaviour
{

    public TextMeshProUGUI giftsCoutText;
    public TextMeshProUGUI timeText;

    private StoryProgressPasser progress;

    void Start() {
        progress = FindObjectOfType<StoryProgressPasser>();
        giftsCoutText.text = progress.gifts.ToString();
        timeText.text = Mathf.Round(progress.timer / 60)  + "m " + Mathf.Round(progress.timer) % 60 + "s ";
    }
}
