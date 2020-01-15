using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkField : Buyable
{
    protected override int chargeForResit()
    {
        return DiceManager.instance.getDieRollResults().Sum() * 2 * getAmountOfNetworksOwnedByOwner();
    }
    int getAmountOfNetworksOwnedByOwner()
    {
        int numberOfNetworksOwnedByOwner = 0;
        foreach (var network in transform.parent.GetComponentsInChildren<NetworkField>())
        {
            if (network.getCurrentOwner() == owner)
            {
                ++numberOfNetworksOwnedByOwner;
            }
        }

        return numberOfNetworksOwnedByOwner;
    }
}