using System.Collections;
using System.Collections.Generic;
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

    public IEnumerator display(Sprite chanceImage)
    {
        return null;
        /*
        image.gameObject.GetComponent<Image>().sprite = chanceImage;      
        
        window.SetActive(true);

        while (!doesPlayerPressButton)
        {
            yield return null;
        }

        window.SetActive(false);
        doesPlayerPressButton = false;
        */
    }

    public void onSettingsButtonClick()
    {
        window.SetActive(true);
    }

    public void onSaveButtonClick()
    {
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
