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

    public enum Action { RollDice, NotSet };

    private Action chosenAction = Action.NotSet;

    public Action getChosen()
    {
        return chosenAction;
    }

    public IEnumerator askPlayerForChoice()
    {
        chosenAction = Action.NotSet;

        var rollButton = transform.GetChild(0).gameObject;

        rollButton.SetActive(true);

        while (chosenAction == Action.NotSet)
        {
            yield return null;
        }

        rollButton.SetActive(false);
    }
    public void roll()
    {
        chosenAction = Action.RollDice;
    }
}
