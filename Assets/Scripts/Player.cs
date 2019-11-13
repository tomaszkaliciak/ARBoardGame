using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int balance = 2000;
    private string playerName;
    private static int playerID = 0;
    private BoardField currentPlace;

    [SerializeField] private Material[] colorsOfPieces;

    public void initialize()
    {
        Debug.LogError("initialize: " + playerName + playerID);

        transform.GetComponent<MeshRenderer>().sharedMaterial = colorsOfPieces[playerID];
        currentPlace = StartField.instance;
        playerID++;
    }

    public void setName(string name) { playerName = name; }

}
