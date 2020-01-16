using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface Chance
{
    Sprite getSprite();
    IEnumerator executeRule(Player player);
}

public class GoToJail : Chance
{
    public Sprite getSprite()
    {
        return Resources.Load("Chances/dummyChance2", typeof(Sprite)) as Sprite;
    }

    public IEnumerator executeRule(Player player)
    {
        player.getToPrison();
        var prisionField = GameObject.FindObjectOfType<Prison>();
        return player.goToField(prisionField);       
    }
}

public class BankFee : Chance
{
    public Sprite getSprite()
    {
        return Resources.Load("Chances/dummyChance2", typeof(Sprite)) as Sprite;
    }

    public IEnumerator executeRule(Player player)
    {
        player.updateBalanceBy(-100);
        return null;       
    }
}

public class PlayerFee : Chance
{
    public Sprite getSprite()
    {
        return Resources.Load("Chances/dummyChance2", typeof(Sprite)) as Sprite;
    }

    public IEnumerator executeRule(Player player)
    {
        foreach (var otherPlayer in GameController.instance.getPlayers().FindAll(p => p != player))
        {
            player.updateBalanceBy(-50);
            otherPlayer.updateBalanceBy(50);
        }
        return null;       
    }
}

public class OwnedCoursesFee : Chance
{
    public Sprite getSprite()
    {
        return Resources.Load("Chances/dummyChance2", typeof(Sprite)) as Sprite;
    }

    public IEnumerator executeRule(Player player)
    {
        int toPay = 0;
        foreach (var ownedCourse in player.getOwnedCourses())
        {
            toPay += ownedCourse.getCurrentLevel() * 20;
        }
        string msg = "Gracz " + player.getName() + " płaci " + toPay + " zł łapówki za milczenie świadków.";
        yield return Alert.instance.displayFormattedAlert(msg, Color.red); 
        player.updateBalanceBy(-toPay);
        yield return null;       
    }
}

public class GoToFreeParking : Chance
{
    public Sprite getSprite()
    {
        return Resources.Load("Chances/dummyChance2", typeof(Sprite)) as Sprite;
    }

    public IEnumerator executeRule(Player player)
    {
        var freeParkingField = GameObject.FindObjectOfType<DummyFieldCorner>();
        yield return player.moveForwardTo(freeParkingField);
    }
}

public class GoToStart : Chance
{
    public Sprite getSprite()
    {
        return Resources.Load("Chances/dummyChance2", typeof(Sprite)) as Sprite;
    }

    public IEnumerator executeRule(Player player)
    {
        var startField = GameObject.FindObjectOfType<StartField>();
        yield return player.moveForwardTo(startField);
    }
}
public class GoBackToStart : Chance
{
    public Sprite getSprite()
    {
        return Resources.Load("Chances/dummyChance2", typeof(Sprite)) as Sprite;
    }

    public IEnumerator executeRule(Player player)
    {
        var startField = GameObject.FindObjectOfType<StartField>();
        yield return player.moveBackwardTo(startField);
    }
}

public class GetOutOfJailCard : Chance
{
    public Sprite getSprite()
    {
        return Resources.Load("Chances/dummyChance2", typeof(Sprite)) as Sprite;
    }

    public IEnumerator executeRule(Player player)
    {
        player.HasPlayerGettingOutOfJailCard = true;
        return null;
    }
}

