using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainField : Buyable
{
    private int resitPrice = 50;
    public override void passThrough(Player player) { }

    protected override int chargeForResit()
    {
        return resitPrice * getAmountOfRailwaysOwnedByOwner();
    }
    int getAmountOfRailwaysOwnedByOwner()
    {
        int numberOfRailwayOwnedByOwner = 0;
        foreach (var railway in transform.parent.GetComponentsInChildren<TrainField>())
        {
            if (railway.getCurrentOwner() == owner)
            {
                ++numberOfRailwayOwnedByOwner;
            }
        }

        return numberOfRailwayOwnedByOwner;
    }
}
