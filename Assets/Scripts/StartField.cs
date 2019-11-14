using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartField : BoardField
{
    public static StartField instance;

    protected override void init() { instance = this; }
}
