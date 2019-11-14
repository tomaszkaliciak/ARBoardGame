using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager instance;

    private Vector3[] initialDiePositions;

    private int[] dieRollResults;
    public int[] getDieRollResults()
    {
        return dieRollResults;
    }

    void Awake()
    {
        instance = this;

        initialDiePositions = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            initialDiePositions[i] = transform.GetChild(i).transform.position;
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public IEnumerator rollDie()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).transform.position = initialDiePositions[i];
            transform.GetChild(i).transform.rotation = UnityEngine.Random.rotation;
        }

        yield return new WaitForSeconds((float)3.5);

        Die[] dies = gameObject.GetComponentsInChildren<Die>();
        dieRollResults = new int[dies.Length];

        for (int i = 0; i < dies.Length; i++)
            dieRollResults[i] = dies[i].getDieValue();
    }
}
