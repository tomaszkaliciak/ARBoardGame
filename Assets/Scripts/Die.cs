using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public int getDieValue()
    {
        var sideOnTop = transform.GetChild(0);

        for (int i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).position.y > sideOnTop.position.y)
            {
                sideOnTop = transform.GetChild(i);
            }
        }

        return Int32.Parse(sideOnTop.gameObject.name);
    }
    
    private void FixedUpdate()
    {
        Vector3 direction = transform.parent.position - transform.position;
        float distance = direction.magnitude;

        if (distance == 0f) return;
        
        float forceMagnitude = 50000000f / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;
        gameObject.GetComponent<Rigidbody>().AddForce(force);
    }
}
