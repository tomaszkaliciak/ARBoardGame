using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardField : MonoBehaviour
{
    public BoardField nextField;

    protected virtual void Awake()
    {
        const int numberOfFields = 40;
        int siblingIndex = transform.GetSiblingIndex();
        var parent = gameObject.transform.parent;
        nextField = siblingIndex < numberOfFields ?
            parent.GetChild(siblingIndex + 1).gameObject.GetComponent<BoardField>() :
            parent.GetChild(1).gameObject.GetComponent<BoardField>();

        init();
    }
    protected virtual void init() {}

    public virtual IEnumerator passThrough(Player player)
    {
        return null;
    }

    public virtual IEnumerator visit(Player player)
    {
        return null;
    }

    public int getIndex()
    {
        return transform.GetSiblingIndex();
    }
}
