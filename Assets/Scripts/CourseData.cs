using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CourseData
{
    public bool mortgage;
    public int ownerID;
    public string courseName;
    public int level;

    public CourseData(Course field)
    {
        mortgage = field.isPledged();
        var owner = field.getCurrentOwner();
        ownerID = owner is null ? -1 : field.getCurrentOwner().getPlayerID();
        courseName = field.getName();
        level = field.getCurrentLevel();
    }
}
