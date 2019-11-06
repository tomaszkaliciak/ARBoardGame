using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<Player> players;
    public static GameController instance;

    void Awake()
    {
        instance = this;

        players = new List<Player>();
    }

    public void registerNewPlayer(string playerName)
    {
        Debug.LogError("register: " + playerName);
    }

    public void startGame()
    {
        Debug.LogError("startGame");
    }
}
