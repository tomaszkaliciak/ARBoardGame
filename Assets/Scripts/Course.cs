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
}
