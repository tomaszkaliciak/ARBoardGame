using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int balance = 2000;
    private string playerName;

    private static int playerID = 0;
    private BoardField currentPlace;

    [SerializeField] private Material[] colorsOfPieces;
    public List<Buyable> ownedFields = new List<Buyable>();

    public void initialize()
    {
        Debug.LogError("initialize: " + playerName + playerID);

        transform.GetComponent<MeshRenderer>().sharedMaterial = colorsOfPieces[playerID];
        currentPlace = StartField.instance;
        playerID++;
    }

    public void setName(string name) { playerName = name; }
    public string getName() { return playerName; }
    public int getBalance() { return balance; }
    public void updateBalanceBy(int amountOfmoney)
    {
        balance += amountOfmoney;
        PlayerInfo.instance.updateBalance(balance);
    }
    public IEnumerator rotate(float degrees)
    {
        float progress = 0;
        float startTime = Time.time;
        float startAngle = transform.eulerAngles.y;

        while (progress <= .98f)
        {
            progress = Time.time - startTime * 0.5f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, startAngle + degrees * progress, transform.eulerAngles.z);

            yield return null;
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, startAngle + degrees, transform.eulerAngles.z);
    }

    public IEnumerator goToField(BoardField field)
    {
        float startTime = Time.time;

        Vector3 startPosition = transform.position; 
        Vector3 endPosition = field.gameObject.transform.position;

        Vector3 displacement = endPosition - startPosition;
        displacement.y = 0;

        float progress = 0;
        
        while (progress <= .98f)
        {
            progress = 5 * (Time.time - startTime);

            Vector3 newPosition = startPosition + displacement * progress;
            newPosition.y = -1 * Mathf.Pow(progress - 0.5f, 2) + 0.25f;

            transform.position = newPosition;

            yield return null;
        }

        currentPlace = field;
        transform.position = currentPlace.transform.position;
    }


    public IEnumerator takeSteps(int spaces)
    {   
        bool movingForward = spaces > 0;
        spaces = Math.Abs(spaces);

        for (int i = 0; i < spaces; i++)
        {
            yield return goToField(currentPlace.nextField);
            currentPlace.passThrough(this);

            if (currentPlace is StartField || currentPlace is DummyFieldCorner)
            {
                yield return rotate(movingForward ? 90 : -90);
            }
        }
        yield return currentPlace.visit(this);
    }
}
