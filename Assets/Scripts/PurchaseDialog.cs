using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseDialog : MonoBehaviour
{
    public static PurchaseDialog instance;
    void Awake()
    {
        instance = this;
    }

    [SerializeField] private Image graphics;

    public bool? playerWantsToBuy;

    public IEnumerator OfferPurchase(Buyable course)
    {
        playerWantsToBuy = null;
        graphics.sprite = course.graphics;
        transform.GetChild(0).gameObject.SetActive(true);

        while (playerWantsToBuy == null)
            yield return null;

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void buy()
    {
        playerWantsToBuy = true;
    }
    public void dontBuy()
    {
        playerWantsToBuy = false;
    }
}
