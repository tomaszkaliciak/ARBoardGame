using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoJailField : BoardField
{
    public override IEnumerator visit(Player player)
    {
        string msg = "Gracz " + player.getName() + " jest zmuszony pójść na dziekankę. " +
                     "Aby wrócić do gry w kolejnej rundzie musi wyrzucić dublet albo zapłacić za warunki."; 
        yield return Alert.instance.displayAlert(msg, Color.red);
        player.getToPrison();
        yield return player.goToField(gameObject.transform.parent.GetChild(11).gameObject.GetComponent<BoardField>());
    }
}
