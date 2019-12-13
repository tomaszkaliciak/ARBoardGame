﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoJailField : BoardField
{
    public override void passThrough(Player player){}

    public override IEnumerator visit(Player player)
    { 
        yield return player.goToField(gameObject.transform.parent.GetChild(11).gameObject.GetComponent<BoardField>());
    }
}
