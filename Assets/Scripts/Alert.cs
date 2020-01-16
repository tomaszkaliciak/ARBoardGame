using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    public static Alert instance;
    void Awake()
    {
        instance = this;
        alertWindow = transform.GetChild(0).gameObject;
    }

    [SerializeField] private Text message;
    [SerializeField] private Button button;
    private GameObject alertWindow;
    private bool doesPlayerPressButton = false;

    public IEnumerator displayAlert(string alert, Color colorOfButton)
    {
        message.text = alert;
        button.gameObject.GetComponent<Image>().color = colorOfButton;
        
        alertWindow.SetActive(true);

        while (!doesPlayerPressButton)
        {
            yield return null;
        }
        
        alertWindow.SetActive(false);
        doesPlayerPressButton = false; 
    }

    public IEnumerator displayFormattedAlert(string alert, Color colorOfButton)
    {
        yield return displayAlert(splitTextToNCharacters(alert, 26), colorOfButton);
    }

    private string splitTextToNCharacters(string text, int n)
    {
        string[] stringSplit = text.Split(' ');
        int charCounter = 0;
        string finalString = "";
 
        for (int i=0; i < stringSplit.Length; i++)
        {
            if (charCounter + stringSplit[i].Length > n)
            {
                finalString += "\n";
                charCounter = 0;
            }
            finalString += stringSplit[i] + " ";
            charCounter += stringSplit[i].Length;
        }
        return finalString; 
    }
    public void onUserPressOKButton()
    {
        doesPlayerPressButton = true;
    }
}
