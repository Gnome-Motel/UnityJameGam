using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{

    public void LoadGameScene() {
        FindObjectOfType<SceneTransition>().LoadScene("Arcade");
    }
}
