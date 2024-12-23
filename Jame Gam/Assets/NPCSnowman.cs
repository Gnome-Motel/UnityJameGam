using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSnowman : NPC
{

    public override void Collect() {
        if (collected) {
            return;
        }

        Debug.Log("SNOWMAN");
        collected = true;
    }
}
