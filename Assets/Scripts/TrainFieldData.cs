using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainFieldData
{
    public bool mortgage;
    public int ownerID;
    public string courseName;

    public TrainFieldData(TrainField field)
    {
        mortgage = field.isPledged();
        var owner = field.getCurrentOwner();
        ownerID = owner is null ? -1 : field.getCurrentOwner().getPlayerID();
        courseName = field.getName();
    }
}
