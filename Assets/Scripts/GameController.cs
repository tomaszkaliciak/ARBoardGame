using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.UNetWeaver;
using UnityEngine;
using Vuforia;

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
        VuforiaBehaviour.Instance.enabled = false;
    }

    public void registerNewPlayer(string playerName)
    {
        Debug.LogError("register: " + playerName);

        Player newPlayer = 
            ((GameObject)(
                Instantiate(
                    playerPrefab,
                    StartField.instance.transform.position,
                    transform.rotation)))
            .GetComponent<Player>();
        
        Vector3 placementOffset = Vector3.zero;

        switch (players.Count)
        {
            case 1:
                placementOffset = 8 * transform.forward;
                break;

            case 2:
                placementOffset = -8 * transform.forward;
                break;

            case 3:
                placementOffset = 8 * transform.right;
                break;
        }
        
        newPlayer.transform.localPosition += placementOffset; 
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
        VuforiaBehaviour.Instance.enabled = true;
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
                    if (PlayerAction.instance.getChosen() == PlayerAction.Action.PayForGettingOutOfPrison)
                    {
                        player.updateBalanceBy(-50);
                        player.getOutOfJail();
                        yield return PlayerAction.instance.askPlayerForChoice(player);
                    }
                    else if (PlayerAction.instance.getChosen() == PlayerAction.Action.GetOutOfPrisonUsingCard)
                    {
                        player.getOutOfJail();
                        player.HasPlayerGettingOutOfJailCard = false;
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

    public List<Player> getPlayers()
    {
        return players;
    }

    private List<Player> getPlayersOrderedByAssets()
    {
        return players.OrderBy(player => player.getPlayerAssets()).Reverse().ToList();
    }
    private string prepareFormattedScoreboard()
    {
        var rankingOfPlayers = getPlayersOrderedByAssets();
        string message = "";
       
        // I want it to be nicely formatted, so every line will have exactly 26 characters
        int numberOfCharacterInOneRow = 27;
        int currentPosition = 0;
        int previousBestScore = 0;
        int numberOfCharactersNeededForDisplayingPosition = 3;
        int numberOfCharactersNeededForUnitAndNewLine = 4;
        
        for (int i = 0; i < rankingOfPlayers.Count; i++)
        {
            var player = rankingOfPlayers[i];
            var score = player.getPlayerAssets();
            
            if (score != previousBestScore)
            {
                previousBestScore = score;
                ++currentPosition;
            }
            
            int numberOfSpacesNeeded = numberOfCharacterInOneRow
                                       - player.getName().Length
                                       - numberOfCharactersNeededForDisplayingPosition
                                       - score.ToString().Length
                                       - numberOfCharactersNeededForUnitAndNewLine;
            
            string alignment = numberOfSpacesNeeded != 0 ? new String(' ', numberOfSpacesNeeded) : "";
            string row = currentPosition + ". " + player.getName() + alignment + score + "zł\n";
            
            message += row;
        }

        return message;
    }
    public IEnumerator showScoreboard()
    {
        yield return Alert.instance.displayAlert(prepareFormattedScoreboard(), Color.blue);
    }
}
