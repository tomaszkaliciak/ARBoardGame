using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    private GameObject window;
    private bool doesPlayerPressButton = false;
    
    void Awake()
    {
        instance = this;
        window = transform.GetChild(0).gameObject;
    }
    public void onSettingsButtonClick()
    {
        window.SetActive(true);
    }

    public void onSaveButtonClick()
    {
        GameSaver.save(GameController.instance.getPlayers());
        GameSaver.save(GameObject.FindObjectsOfType<Course>().ToList());
        GameSaver.save(GameObject.FindObjectsOfType<NetworkField>().ToList());
        GameSaver.save(GameObject.FindObjectsOfType<TrainField>().ToList());
    }

    public void onSummaryButtonClick()
    {
        StartCoroutine(GameController.instance.showScoreboard());
    }

    public void onUndoButtonClick()
    {
        window.SetActive(false);
    }
    
    public void onExitGameButtonClick()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
