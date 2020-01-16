using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buyable : BoardField
{
    protected Player owner;

    protected bool mortgage = false;
    [SerializeField] private int purchasePrice;
    [SerializeField] private string courseName;
    [SerializeField] public Sprite graphics;

    //TODO: it shouldn't display FieldInfo when for example player is asked if he wants buy a new field
    Collider coll;

    // TODO: cleaup (https://answers.unity.com/questions/527665/mouse-click-a-collision-mesh.html)
    void Start()
    {
        coll = this.gameObject.GetComponent<Collider>();

        MeshFilter r = this.gameObject.GetComponent<MeshFilter>();
        if (r != null)
        {
            Mesh m = r.sharedMesh;
            if (m != null)
            {
                int[] tris = m.triangles;
                for (int i = 0; i < tris.Length; i += 3)
                {
                    int t = tris[i];
                    tris[i] = tris[i + 1];
                    tris[i + 1] = t;
                }
                m.triangles = tris;
            }
        }
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (coll.Raycast(ray, out hit, 100.0f) && hit.collider.gameObject == gameObject)
            {
                FieldInfo.instance.display(this);
            }
        }
    }
  
    public override IEnumerator visit(Player player)
    {
        Debug.Log("visit ");
        if (player.getBalance() > this.purchasePrice && owner == null)
        {
            yield return PurchaseDialog.instance.OfferPurchase(this);

            if (PurchaseDialog.instance.playerWantsToBuy.GetValueOrDefault())
            {
                player.updateBalanceBy(-purchasePrice);
                owner = player;

                var playerColor = player.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
                var outline = transform.GetChild(0);
                outline.gameObject.GetComponent<MeshRenderer>().sharedMaterial = playerColor;
                outline.gameObject.SetActive(true);
                string msg = "Gracz " + player.getName() + " zakupił pole " + courseName + " za " + purchasePrice  + "zł.";
                yield return Alert.instance.displayFormattedAlert(msg, Color.blue); 
            }
        }
        else if (owner && owner != player && !this.mortgage)
        {
            int cost = chargeForResit();
            string msg = "Gracz " + player.getName() + " płaci graczowi " + owner.getName() + " " + cost +
                         "zł za stanięcie na polu " + courseName; 
            yield return Alert.instance.displayFormattedAlert(msg, Color.red);
            player.updateBalanceBy(-cost);
            owner.updateBalanceBy(cost);
        }
    }
  
    public void takeMortgage()
    {
        if (mortgage || owner is null) return;
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        owner.updateBalanceBy(purchasePrice/2);
        mortgage = true;
    }
    public void repayMortgage()
    {
        if (!mortgage || owner is null) return;
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        owner.updateBalanceBy(-purchasePrice/2);
        mortgage = false;
    }
    
    public Player getCurrentOwner()
    {
        return owner;
    }

    public bool isPledged()
    {
        return mortgage;
    }

    protected abstract int chargeForResit();

    public int getPurchasePrice()
    {
        return purchasePrice;
    }

    public string getName()
    {
        return courseName;
    }
}
