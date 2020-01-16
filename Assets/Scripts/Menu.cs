using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{   
    [SerializeField] private InputField[] playerPanels = new InputField[4];
    [SerializeField] private GameObject thirdPlayerCreator, fourthPlayerCreator;

    private void Start()
    {
        if (filesExist())
        {
            transform.GetChild(5).gameObject.SetActive(true);
        }
    }

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

    public void exitGameClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif 
    }
    public void loadGameClicked()
    {
        var savedPlayers = GameSaver.loadPlayers();
        for (int i = 0; i < savedPlayers.Count; i++)
        {
            GameController.instance.registerNewPlayer(getPlayerName(i, playerPanels[i]));
        }

        foreach (var player in savedPlayers)
        {
            GameController.instance.loadPlayer(player);
        }
        foreach (var trainField in GameSaver.loadTrainFields())
        {
            GameController.instance.loadField(trainField);
        }
        foreach (var networkField in GameSaver.loadNetworkFields())
        {
            GameController.instance.loadField(networkField);
        } 
        foreach (var courseField in GameSaver.loadCourses())
        {
            GameController.instance.loadField(courseField);
        }
        
        GameController.instance.startGame();
        
        gameObject.SetActive(false);
    }

    bool filesExist()
    {
        var path = Application.persistentDataPath;
        return File.Exists(path + "/players.bin");
    }
}
