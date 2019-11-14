using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardField : MonoBehaviour
{
    [HideInInspector] public BoardField nextField;

    private void Awake()
    {
        const int numberOfFields = 40;
        int siblingIndex = transform.GetSiblingIndex();
        
        nextField = siblingIndex < numberOfFields ?
            gameObject.transform.parent.GetChild(siblingIndex + 1).gameObject.GetComponent<BoardField>() :
            gameObject.transform.parent.GetChild(1).gameObject.GetComponent<BoardField>();

        init();
    }
    protected virtual void init() { }
}
