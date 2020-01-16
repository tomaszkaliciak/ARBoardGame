using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaidParking : BoardField
{
    [SerializeField] private int cost;

    public override IEnumerator visit(Player player)
    {
        player.updateBalanceBy(-cost);
        string msg = "Gracz " + player.getName() + " płaci " + cost + " zł za nielegalne parkowanie pod C1.";
        yield return Alert.instance.displayFormattedAlert(msg, Color.red); 
    }
}
