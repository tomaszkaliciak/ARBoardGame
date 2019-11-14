using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{   
    [SerializeField] private InputField[] playerPanels = new InputField[4];
    [SerializeField] private GameObject thirdPlayerCreator, fourthPlayerCreator;

    private string getPlayerName(int num, InputField nameField)
    {
        if (nameField.text.Equals(""))
        {
            return "Player " + num;
        }
        return nameField.text;
    }
    
    public void startGameClicked()
    {
        for (int i = 0; i < 4; i++)
        {
            if ((i == 2 && !thirdPlayerCreator.activeSelf) || (i == 3 && !fourthPlayerCreator.activeSelf))
            {
                continue;
            }
            
            GameController.instance.registerNewPlayer(getPlayerName(i, playerPanels[i]));
        }

        GameController.instance.startGame();
        
        gameObject.SetActive(false);
    }
}
