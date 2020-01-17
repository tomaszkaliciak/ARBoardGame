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
        balance = GameObject.Find("PlayerInfo").transform.Find("Balance").GetComponent<Text>();
        playerName = GameObject.Find("PlayerInfo").transform.Find("Name").GetComponent<Text>();
    }

    public void updateBalance(int newbalance)
    {
        Awake();
        try
        {
            this.balance.text = newbalance.ToString();
        }
        catch
        {
        }
    }

    public void setPlayer(Player currentPlayer)
    {
        this.playerName.text = currentPlayer.getName();
        this.balance.text = currentPlayer.getBalance().ToString();
        var playerColor = currentPlayer.gameObject.GetComponent<MeshRenderer>().sharedMaterial.color;
        playerColor.a = 0.5f;
        gameObject.GetComponent<Image>().color = playerColor;
    }
}
