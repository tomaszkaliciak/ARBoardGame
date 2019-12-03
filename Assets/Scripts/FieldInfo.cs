using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FieldInfo : MonoBehaviour
{
    public static FieldInfo instance;
    private GameObject panel;
    private GameObject fieldDetail;
    private GameObject mortgageButton;
    void Awake()
    {
        instance = this;
        panel = transform.GetChild(0).gameObject;
        fieldDetail = panel.transform.GetChild(0).gameObject;
        mortgageButton = transform.GetChild(1).gameObject; 
    }

    [SerializeField] private Sprite graphics;
    
    public void display(Buyable field)
    {
        graphics = field.graphics;
        
        if (field.GetType() == typeof(Course))
        {
            var couseField = (Course) field;
            var currentLevel = couseField.getCurrentLevel().ToString();
            
            fieldDetail.GetComponentInChildren<Text>().text = "Poziom ulepszeń:" + currentLevel;
        }
        else
        {
            fieldDetail.GetComponentInChildren<Text>().text = "";
        }
        displayMortageButton(field);
        panel.SetActive(true);
    }

    private void displayMortageButton(Buyable field)
    {
        var owner = field.getCurrentOwner();

        if (owner != GameController.instance.getCurrentPlayer())
        {
            return;
        }

        var textOnMortageButton = mortgageButton.GetComponentInChildren<Text>();

        textOnMortageButton.text = field.isPledged() ? "Wykup pole" : "Zastaw pole";
        mortgageButton.GetComponent<Button>().onClick.RemoveAllListeners(); 
        mortgageButton.GetComponent<Button>().onClick.AddListener(
            new UnityAction(() => onMortgageButtonClick(field)));
        mortgageButton.SetActive(true);
    }
    public void hidePanel()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void onMortgageButtonClick(Buyable field)
    {
        if (field.isPledged())
        {
            field.repayMortgage();
            mortgageButton.GetComponentInChildren<Text>().text = "Zastaw pole";
        }
        else
        {
            field.takeMortgage();
            mortgageButton.GetComponentInChildren<Text>().text = "Wykup pole";
        }
    }
}
