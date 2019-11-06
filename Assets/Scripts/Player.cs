using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int balance = 2000;
    private string playerName;
    private static int numberOfPlayers = 0;

    public Player(string name)
    {
        playerName = name;
        ++numberOfPlayers;
    }
}
