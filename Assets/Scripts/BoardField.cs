using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardField : MonoBehaviour
{
    [HideInInspector] public BoardField nextField;

    private void Awake()
    {
        int currentSpace = Int32.Parse(gameObject.name);

        nextField = currentSpace < 39 ?
            gameObject.transform.parent.Find((currentSpace + 1).ToString()).GetComponent<BoardField>() :
            gameObject.transform.parent.Find("0").GetComponent<BoardField>();

        init();
    }
    protected virtual void init() { }
}
