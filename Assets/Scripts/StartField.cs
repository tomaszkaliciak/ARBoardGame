using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartField : BoardField
{
    public static StartField instance;

    protected override void init() { instance = this; }

    public override IEnumerator passThrough(Player player)
    {
        string msg = "Gracz " + player.getName() + " otrzymuje 200zł za przetrwanie semestru.";
        yield return Alert.instance.displayFormattedAlert(msg, Color.red); 
        player.updateBalanceBy(200);
        yield return null;
    }
}
