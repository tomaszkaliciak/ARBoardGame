using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buyable : BoardField
{
    protected Player owner;
    public enum Department { K1, K2, K3, K4, K5, K6, K7, K8, NA };

    [SerializeField] private Department department;
    [SerializeField] private int resitPrice;
    [SerializeField] private int purchasePrice;
    [SerializeField] private string courseName;
    [SerializeField] public Sprite graphics;
    
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
            if (coll.Raycast(ray, out hit, 1000.0f))
            {
                FieldInfo.instance.display(this);
            }
        }
    }
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
