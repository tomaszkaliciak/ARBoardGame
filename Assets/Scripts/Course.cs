using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course : Buyable
{
    public enum Department { K1, K2, K3, K4, K5, K6, K7, K8 };

    [SerializeField] private Department department;
    [SerializeField] private int housePrice;
    [SerializeField] private int[] resitPrices = new int[6];

    private int currentLevel = 0;

    public Department getDepartment()
    {
        return department;
    }

    protected override int chargeForResit()
    {
        if (currentLevel == 0 && owner && owner.doesPlayerOwnADepartment(this))
        {
            return resitPrices[0] * 2;
        }
        return resitPrices[currentLevel];
    }
    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public int getUpgradeCost()
    {
        return housePrice;
    }

    public void upgradeField()
    {
        transform.GetChild(2).gameObject.transform.GetChild(currentLevel).gameObject.SetActive(true);
        ++currentLevel;
    }

    public void downgradeField()
    {
        --currentLevel;
        transform.GetChild(2).gameObject.transform.GetChild(currentLevel).gameObject.SetActive(false);
    }
    public void loadFromCourseData(CourseData data)
    {
        mortgage = data.mortgage;
        if (data.ownerID == -1)
        {
            return;
        }
        owner = GameController.instance.getPlayers().Find(s => s.getPlayerID() == data.ownerID);
       
        var playerColor = owner.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        var outline = transform.GetChild(0);
        outline.gameObject.GetComponent<MeshRenderer>().sharedMaterial = playerColor;
        outline.gameObject.SetActive(true);

        for (int i = 0; i < data.level; ++i)
        {
            upgradeField();
        }
        gameObject.transform.GetChild(1).gameObject.SetActive(mortgage);
    }
}
