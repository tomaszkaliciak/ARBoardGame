using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyField : Buyable
{
    private int resitPrice;

    public override void passThrough(Player player) { }

    protected override int chargeForResit()
    {
        return resitPrice;
    }
}
