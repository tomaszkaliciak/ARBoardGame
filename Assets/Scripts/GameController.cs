using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<Player> players;
    private Player currentPlayer;
    public static GameController instance;

    [SerializeField] private GameObject playerPrefab;

    void Awake()
    {
        instance = this;

        players = new List<Player>();
    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void registerNewPlayer(string playerName)
    {
        Debug.LogError("register: " + playerName);

        Vector3 placementOffset = Vector3.zero;

        switch (players.Count)
        {
            case 1:
                placementOffset = new Vector3(.7f, 0, 0);
                break;

            case 2:
                placementOffset = new Vector3(-.7f, 0, 0);
                break;

            case 3:
                placementOffset = new Vector3(0, 0, -.7f);
                break;
        }
        
        Player newPlayer = 
            ((GameObject)(
                Instantiate(
                    playerPrefab,
                    StartField.instance.transform.position + placementOffset,
                    transform.rotation)))
            .GetComponent<Player>();

        newPlayer.transform.parent = transform.parent.GetChild(0).transform; 
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
                currentPlayer = player;
                PlayerInfo.instance.setPlayer(player);
                int[] dieRollResults = new int[2];
                do
                {
                    yield return PlayerAction.instance.askPlayerForChoice(player);
                    if (PlayerAction.instance.getChosen() == PlayerAction.Action.GetOutOfPrison)
                    {
                        player.updateBalanceBy(-50);
                        player.getOutOfJail();
                        yield return PlayerAction.instance.askPlayerForChoice(player);
                    }

                    var userChoice = PlayerAction.instance.getChosen();
                    
                    if (userChoice == PlayerAction.Action.RollDice)
                    {
                        yield return DiceManager.instance.rollDie();
                        dieRollResults = DiceManager.instance.getDieRollResults();
                        Debug.Log("Got " + dieRollResults.Sum());

                        if (player.isInPrison())
                        {
                            if (dieRollResults.Length != dieRollResults.Distinct().Count())
                            {
                                player.getOutOfJail();
                                yield return player.takeSteps(dieRollResults.Sum());
                            }
                            else
                            {
                                player.stayInPrisonForThisRound();
                            }
                        }
                        else
                        {
                            yield return player.takeSteps(dieRollResults.Sum());
                        }
                    }
                } while (dieRollResults.Length != dieRollResults.Distinct().Count());
            }
        }
    }

    public Player getCurrentPlayer()
    {
        return currentPlayer;
    }
}
