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

    public void loadFromNetworkFieldData(NetworkFieldData data)
    {
        mortgage = data.mortgage;
        if (data.ownerID == -1)
        {
            return;
        }

        owner = GameController.instance.getPlayers().Find(s => s.getPlayerID() == data.ownerID);
        var playerColor = owner.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        var outline = transform.GetChild(0);
        outline.gameObject.GetComponent<MeshRenderer>().sharedMaterial = playerColor;
        outline.gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(mortgage);
    }
}