using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public int getDieValue()
    {
        var sideOnTop = transform.GetChild(0);
        var board = transform.parent.parent;
        
        for (int i = 1; i < transform.childCount; i++)
        {
            var positionOfCurrentHigherSideRelativeToBoard = board.InverseTransformPoint(sideOnTop.position);
            var positionOfNewSideRelativeToBoard = board.InverseTransformPoint(transform.GetChild(i).position);
 
            if (positionOfNewSideRelativeToBoard.y > positionOfCurrentHigherSideRelativeToBoard.y)
            {
                sideOnTop = transform.GetChild(i);
            }
        }

        return Int32.Parse(sideOnTop.gameObject.name);
    }
    
    private void FixedUpdate()
    {
        var directionTowardsBoard = transform.localPosition;
        directionTowardsBoard.x = 0f;
        directionTowardsBoard.z = 0f;
        Vector3 directionFromWorldPOV = transform.parent.TransformDirection(directionTowardsBoard);
        float distance = directionTowardsBoard.y;

        if (distance == 0f) return;

        float forceMagnitude = 150f/Mathf.Pow(distance+1f, 2);
        Vector3 force = directionFromWorldPOV.normalized * -forceMagnitude;
        gameObject.GetComponent<Rigidbody>().AddForce(force);
    }
}
