using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainField : Buyable
{
    private int resitPrice = 50;
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
    
    public void loadFromTrainFieldData(TrainFieldData data)
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
