using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public static PlayerAction instance;
    void Awake()
    {
        instance = this;
    }

    public enum Action { RollDice, PayForGettingOutOfPrison, GetOutOfPrisonUsingCard, NotSet };

    private Action chosenAction = Action.NotSet;

    public Action getChosen()
    {
        return chosenAction;
    }

    public IEnumerator askPlayerForChoice(Player player)
    {
        chosenAction = Action.NotSet;
        
        var rollButton = transform.GetChild(0).gameObject;
        var getOutOfPrisonButton = transform.GetChild(1).gameObject;
        var useCardButton = transform.GetChild(2).gameObject;

        rollButton.SetActive(true);

        if (player.isInPrison())
        {
            getOutOfPrisonButton.SetActive(true);

            if (player.HasPlayerGettingOutOfJailCard)
            {
                useCardButton.SetActive(true); 
            }
        }

        while (chosenAction == Action.NotSet)
        {
            yield return null;
        }

        if (chosenAction == Action.PayForGettingOutOfPrison)
        {
            string msg = "Gracz " + player.getName() + " płaci za wyjście z dziekanki.";
            yield return Alert.instance.displayAlert(msg, Color.blue); 
        }
        else if (chosenAction == Action.GetOutOfPrisonUsingCard)
        {
            string msg = "Gracz " + player.getName() + " uniknął dziekanki przy użyciu karty specjalnej.";
            yield return Alert.instance.displayAlert(msg, Color.blue); 
        }
        
        rollButton.SetActive(false);
        useCardButton.SetActive(false);
        getOutOfPrisonButton.SetActive(false);
    }
    public void roll()
    {
        chosenAction = Action.RollDice;
    }

    public void payForGettingOutOfPrison()
    {
        chosenAction = Action.PayForGettingOutOfPrison;
    }
    
    public void useCard()
    {
        chosenAction = Action.GetOutOfPrisonUsingCard;
    }
}
