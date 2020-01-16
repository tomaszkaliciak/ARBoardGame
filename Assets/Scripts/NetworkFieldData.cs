using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkFieldData
{
    public bool mortgage;
    public int ownerID;
    public string courseName;

    public NetworkFieldData(NetworkField field)
    {
        mortgage = field.isPledged();
        var owner = field.getCurrentOwner();
        ownerID = owner is null ? -1 : field.getCurrentOwner().getPlayerID();
        courseName = field.getName();
    }
}
