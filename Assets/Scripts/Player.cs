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
    private int numberOfRoundsInPrisonLeft = 0;
    [SerializeField] private Material[] colorsOfPieces;
    public List<Buyable> ownedFields = new List<Buyable>();

    public void initialize()
    {
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
        float startAngle = transform.localEulerAngles.y;

        while (progress <= .98f)
        {
            progress = Time.time - startTime * 0.5f;
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x, 
                startAngle + degrees * progress,
                transform.localEulerAngles.z);

            yield return null;
        }

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x, 
            startAngle + degrees,
            transform.localEulerAngles.z);
    }

    public IEnumerator goToField(BoardField field)
    {
        float startTime = Time.time;

        Vector3 startPosition = transform.localPosition; 
        Vector3 endPosition = field.gameObject.transform.localPosition;

        Vector3 displacement = endPosition - startPosition;
        displacement.y = 0;

        float progress = 0;
        
        while (progress <= .98f)
        {
            progress = 5 * (Time.time - startTime);

            Vector3 newPosition = startPosition + displacement * progress;
            newPosition.y = -1 * Mathf.Pow(progress - 0.5f, 2) + 0.25f;

            transform.localPosition = newPosition;

            yield return null;
        }

        currentPlace = field;
        transform.localPosition = currentPlace.transform.localPosition;
    }


    public IEnumerator takeSteps(int spaces)
    {   
        bool movingForward = spaces > 0;
        spaces = Math.Abs(spaces);

        for (int i = 0; i < spaces; i++)
        {
            yield return goToField(currentPlace.nextField);
            yield return currentPlace.passThrough(this);

            if (currentPlace is StartField || currentPlace is DummyFieldCorner)
            {
                yield return rotate(movingForward ? 90 : -90);
            }
        }
        yield return currentPlace.visit(this);
    }

    public bool doesPlayerOwnADepartment(Course course)
    {
        uint numberOfCoursesFromTheSameDepartment = 0;
        uint numberOfCoursesFromTheSameDepartmentOwnedByPlayer = 0;

        foreach (var c in course.transform.parent.GetComponentsInChildren<Course>())
        {
            if (c.getDepartment() == course.getDepartment())
            {
                var currentOwner = c.getCurrentOwner();
                if (currentOwner && currentOwner == this)
                {
                    ++numberOfCoursesFromTheSameDepartmentOwnedByPlayer;
                }
                ++numberOfCoursesFromTheSameDepartment;
            }
        }

        return numberOfCoursesFromTheSameDepartment == numberOfCoursesFromTheSameDepartmentOwnedByPlayer;
    }

    public bool isInPrison()
    {
        return numberOfRoundsInPrisonLeft > 0;
    }

    public void stayInPrisonForThisRound()
    {
        numberOfRoundsInPrisonLeft--;
    }

    public void getOutOfJail()
    {
        numberOfRoundsInPrisonLeft = 0;
    }

    public void getToPrison()
    {
        numberOfRoundsInPrisonLeft = 3;
    }

    public BoardField getOccupiedField()
    {
        return currentPlace;
    }
    public bool HasPlayerGettingOutOfJailCard { get; set; } = false;

    public Course[] getOwnedCourses()
    {
        return Array.FindAll(GameObject.FindObjectsOfType<Course>(), (course => course.getCurrentOwner() == this));
    }

    public Buyable[] getOwnedFields()
    {
        return Array.FindAll(GameObject.FindObjectsOfType<Buyable>(), (course => course.getCurrentOwner() == this));
    }
    public int getPlayerAssets()
    {
        int assets = balance;

        foreach (var ownedField in getOwnedFields())
        {
            var purchasePrice = ownedField.getPurchasePrice();
            
            if (ownedField.isPledged())
            {
                assets += purchasePrice / 2;
            }
            else
            {
                assets += purchasePrice;
                if (ownedField is Course)
                {
                    Course ownedCourse = (Course)ownedField;
                    assets += ownedCourse.getCurrentLevel() * ownedCourse.getUpgradeCost();
                }
            }
        } 
        
        return assets;
    }
}
