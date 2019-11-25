using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldInfo : MonoBehaviour
{
    public static FieldInfo instance;
    void Awake()
    {
        instance = this;
    }

    [SerializeField] private Sprite graphics;

    public void display(Buyable field)
    {
        graphics = field.graphics;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void hidePanel()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
