using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<Player> players;
    public static GameController instance;

    [SerializeField] private GameObject playerPrefab;

    void Awake()
    {
        instance = this;

        players = new List<Player>();
    }

    public void registerNewPlayer(string playerName)
    {
        Debug.LogError("register: " + playerName);

        Vector3 placementOffset = Vector3.zero;

        switch (players.Count)
        {
            case 1:
                placementOffset = new Vector3(.3f, 0, 0);
                break;

            case 2:
                placementOffset = new Vector3(-.3f, 0, 0);
                break;

            case 3:
                placementOffset = new Vector3(0, 0, -.3f);
                break;
        }
        
        Player newPlayer = 
            ((GameObject)(
                Instantiate(
                    playerPrefab,
                    StartField.instance.transform.position + placementOffset,
                    playerPrefab.transform.rotation)))
            .GetComponent<Player>();

        newPlayer.setName(playerName);

        players.Add(newPlayer);
        newPlayer.initialize();
    }

    public void startGame()
    {
        Debug.LogError("startGame");
        StartCoroutine(playGame());
    }

    private IEnumerator playGame()
    {
        while (true)
        {
            foreach (var player in players)
            {
                yield return DiceManager.instance.rollDie();
                int[] dieRollResults = DiceManager.instance.getDieRollResults();
                Debug.Log("Got " + dieRollResults.Sum());

                yield return player.takeSteps(dieRollResults.Sum());
            }
        }
    }
}
