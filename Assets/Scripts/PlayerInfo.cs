using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    private Text balance, playerName;
    void Awake()
    {
        instance = this;
        balance = transform.Find("Balance").GetComponent<Text>();
        playerName = transform.Find("Name").GetComponent<Text>();
    }

    public void updateBalance(int balance)
    {
        this.balance.text = balance.ToString();
    }

    public void setPlayer(Player currentPlayer)
    {
        this.playerName.text = currentPlayer.name;
        this.balance.text = currentPlayer.getBalance().ToString();
        var playerColor = currentPlayer.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = playerColor;
    }
}
