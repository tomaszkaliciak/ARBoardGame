using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int balance;
    public string playerName;
    public int playerID;
    public int sibilingIndexOfCurrentPlace;
    public int numberOfRoundsInPrisonLeft;

    public PlayerData(Player player)
    {
        balance = player.getBalance();
        playerName = player.getName();
        playerID = player.getPlayerID();
        sibilingIndexOfCurrentPlace = player.getOccupiedField().getIndex();
        numberOfRoundsInPrisonLeft = player.getNumberOfRoundsInPrisonLeft();
    }
}
