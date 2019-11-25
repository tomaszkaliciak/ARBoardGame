using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course : Buyable
{
    public enum Department { K1, K2, K3, K4, K5, K6, K7, K8 };

    [SerializeField] private Department department;
    [SerializeField] private int housePrice;
    [SerializeField] private int[] resitPrices = new int[6];

    private int currentLevel;

    public override void passThrough(Player player) {}

    protected override int chargeForResit()
    {
        return resitPrices[currentLevel];
    }
}
