using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/
public static class IListExtensions {
    public static void Shuffle<T>(this IList<T> ts) {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
public class ChanceField : BoardField
{
    static private List<Chance> stackOfCards = new List<Chance>();

    protected override void Awake()
    {
        base.Awake();
        if (stackOfCards.Count == 0)
        {
            fillStack();
        }
    }
    public override IEnumerator visit(Player player)
    {
        if (stackOfCards.Count == 0)
        {
            fillStack();
        }
        
        Chance selectedChance = stackOfCards[0];
        stackOfCards.RemoveAt(0);
        
        var material = selectedChance.getSprite();
        yield return ChanceViewer.instance.display(material);
        yield return selectedChance.executeRule(player);
    }

    void fillStack()
    {
        stackOfCards.Add(new GoToJail());
        stackOfCards.Add(new BankFee());
        stackOfCards.Add(new PlayerFee());
        stackOfCards.Add(new OwnedCoursesFee());
        stackOfCards.Add(new GoToFreeParking());
        stackOfCards.Add(new GoToStart());
        stackOfCards.Add(new GoBackToStart());
        stackOfCards.Add(new GetOutOfJailCard());
        stackOfCards.Shuffle();
    }
}
