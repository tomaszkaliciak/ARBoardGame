using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaidParking : BoardField
{
    [SerializeField] private int cost;

    public override IEnumerator visit(Player player)
    {
        player.updateBalanceBy(-cost);
        return null;
    }
}
