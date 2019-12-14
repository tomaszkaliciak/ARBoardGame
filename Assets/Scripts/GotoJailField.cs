using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoJailField : BoardField
{
    public override IEnumerator visit(Player player)
    {
        player.getToPrison();
        yield return player.goToField(gameObject.transform.parent.GetChild(11).gameObject.GetComponent<BoardField>());
    }
}
