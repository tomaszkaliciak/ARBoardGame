using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FieldInfo : MonoBehaviour
{
    public static FieldInfo instance;
    private GameObject panel;
    private GameObject fieldDetail;
    private GameObject mortgageButton;
    private GameObject upgradeFieldButton;
    private GameObject downgradeFieldButton;
    private GameObject text;
    void Awake()
    {
        instance = this;
        panel = transform.GetChild(0).gameObject;
        fieldDetail = panel.transform.GetChild(0).gameObject;
        mortgageButton = transform.GetChild(1).gameObject;
        upgradeFieldButton = transform.GetChild(2).gameObject;
        downgradeFieldButton = transform.GetChild(3).gameObject;
        text = transform.GetChild(4).gameObject;
    }

    public void display(Buyable field)
    {
        fieldDetail.GetComponent<Image>().sprite = field.graphics;
        
        if (field.GetType() == typeof(Course))
        {
            var couseField = (Course) field;
            var owner = field.getCurrentOwner();
            if (owner == GameController.instance.getCurrentPlayer() &&
                owner.doesPlayerOwnADepartment(couseField))
            {
                text.SetActive(true);
                updateButtons(couseField);
            }
        }
        displayMortageButton(field);
        panel.SetActive(true);
    }
    
    private void updateButtons(Course field)
    {
        var currentLevel = field.getCurrentLevel();
        if (currentLevel > 0)
        {
            displayDowngradeFieldButton(field);
        }
        else
        {
            downgradeFieldButton.SetActive(false);
        }
        
        if (currentLevel < 5)
        {
            displayUpgradeFieldButton(field);
        }
        else
        {
            upgradeFieldButton.SetActive(false);
        }
    }
    private void displayUpgradeFieldButton(Course field)
    {
        var owner = field.getCurrentOwner();

        if (owner != GameController.instance.getCurrentPlayer() ||
            field.GetType() != typeof(Course))
        {
            return;
        }

        if (field.getUpgradeCost() > owner.getBalance())
        {
            return;
        }
        
        upgradeFieldButton.GetComponent<Button>().onClick.RemoveAllListeners(); 
        upgradeFieldButton.GetComponent<Button>().onClick.AddListener(
            new UnityAction(() => onUpgradeButtonClick(field)));
        upgradeFieldButton.SetActive(true);
    }
    
    private void displayDowngradeFieldButton(Course field)
    {
        var owner = field.getCurrentOwner();

        if (owner != GameController.instance.getCurrentPlayer() ||
            field.GetType() != typeof(Course))
        {
            return;
        }

        downgradeFieldButton.GetComponent<Button>().onClick.RemoveAllListeners(); 
        downgradeFieldButton.GetComponent<Button>().onClick.AddListener(
            new UnityAction(() => onDowngradeButtonClick(field)));
        downgradeFieldButton.SetActive(true);
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
        mortgageButton.GetComponent<LeanButton>().OnClick.RemoveAllListeners();
        mortgageButton.GetComponent<LeanButton>().OnClick.AddListener(
            new UnityAction(() => onMortgageButtonClick(field)));
        mortgageButton.SetActive(true);
    }
    public void hidePanel()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        text.SetActive(false);
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

    public void onUpgradeButtonClick(Course field)
    {
        var owner = field.getCurrentOwner();

        owner.updateBalanceBy(-field.getUpgradeCost());
        field.upgradeField();
        updateButtons(field);
    }
    
    public void onDowngradeButtonClick(Course field)
    {
        var owner = field.getCurrentOwner();

        owner.updateBalanceBy(field.getUpgradeCost());
        field.downgradeField();
        updateButtons(field);
    }
}
