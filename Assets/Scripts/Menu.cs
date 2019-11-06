using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{   
    [SerializeField] private InputField[] selectionPanels = new InputField[4];
    [SerializeField] private GameObject selectionPanel3Parent, selectionPanel4Parent;

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
            if ((i == 2 && !selectionPanel3Parent.activeSelf) || (i == 3 && !selectionPanel4Parent.activeSelf))
            {
                continue;
            }
            
            GameController.instance.registerNewPlayer(getPlayerName(i, selectionPanels[i]));
        }

        GameController.instance.startGame();
        
        gameObject.SetActive(false);
    }
}
