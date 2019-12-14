using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyField : Buyable
{
    private int resitPrice;

    protected override int chargeForResit()
    {
        return resitPrice;
    }
}
