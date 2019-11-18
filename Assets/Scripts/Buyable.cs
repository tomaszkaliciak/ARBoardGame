using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buyable : BoardField
{
    protected Player owner;

    [SerializeField] private int resitPrice;
    [SerializeField] private int purchasePrice;
    [SerializeField] private string courseName;
    [SerializeField] public Sprite graphics;

    public override void passThrough(Player player)
    { }

    public override IEnumerator visit(Player player)
    {
        Debug.Log("visit ");

        if (player.getBalance() > this.purchasePrice && owner == null)
        {
            yield return PurchaseDialog.instance.OfferPurchase(this);

            if (PurchaseDialog.instance.playerWantsToBuy.GetValueOrDefault())
            {
                player.updateBalanceBy(-purchasePrice);
                player.ownedFields.Add(this);
                owner = player;

                var playerColor = player.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
                var outline = transform.GetChild(0);
                outline.gameObject.GetComponent<MeshRenderer>().sharedMaterial = playerColor;
                outline.gameObject.SetActive(true);
            }
        }
        else if (owner && owner != player)
        {
            Debug.Log(player.name + "is not owner");

            int cost = chargeForResit();
            player.updateBalanceBy(-cost);
            owner.updateBalanceBy(cost);
        }
    }

    protected int chargeForResit()
    {
        return resitPrice;
    }
}
