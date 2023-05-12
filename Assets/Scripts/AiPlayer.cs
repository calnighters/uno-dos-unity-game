using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class AiPlayer : MonoBehaviour, PlayerInterface
{

    bool drew = false;
    string name;
    List<Card> handList = new List<Card>();

    public AiPlayer(string name)
    {
        this.name = name;
    }

    // Take the turn for an AI user
    public void turn(List<Card> discardPile)
    {
        Control control = GameObject.Find("Control").GetComponent<Control>();
        Card currDisc = discardPile[discardPile.Count - 1];
        string colorDisc = currDisc.getColor();
        int numbDisc = currDisc.getNumb();

        List<int> playableCards = findPlayableCards(currDisc);

        int cardPlayedIndex = -1;
        if (playableCards.Count > 0)
        {
            System.Random random = new System.Random();
            int randomIndex = random.Next(playableCards.Count());
            cardPlayedIndex = playableCards[randomIndex];
        }
        else
        {
            control.drawCard(1, this);
            drew = true;
        }

        if (drew)
        {
            return;
        }

        turnEnd(cardPlayedIndex);
    }

    // Add card to players hand
    public void addCards(Card card)
    {
        handList.Add(card);
    }

    public void turnEnd(int cardIndex)
    { //ends the turn
        Control control = GameObject.Find("Control").GetComponent<Control>();
        if (drew)
        {
            control.updateLog(string.Format("{0} drew", name));
            control.enabled = true;
            return;
        }

        Card card = handList[cardIndex];
        int cardNumber = card.getNumb();
        switch (cardNumber)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                control.updateLog(string.Format("{0} played a {1} {2}", name, card.getColor(), card.getNumb()));
                break;
            case 10:
                control.playReset();
                control.updateLog(string.Format("{0} played a {1} reset card", name, card.getColor()));
                break;
            case 11:
                control.loseTwoCards(loseTwoCards(cardIndex));
                control.updateLog(string.Format("{0} played a {1} lose two card", name, card.getColor()));
                break;
            case 12:
                control.swapDeck();
                control.updateLog(string.Format("{0} played a {1} swap deck card", name, card.getColor()));
                break;
            case 13:
                control.playSeeThrough();
                control.updateLog(string.Format("{0} played a see through card", name));
                break;
        }

        control.updateDiscardPile(card);
        handList.RemoveAt(cardIndex);
    }
    public bool Equals(PlayerInterface other)
    { //equals
        return other.getName().Equals(name);
    }
    public string getName()
    { //returns the name
        return name;
    }
    public int getCardsLeft()
    { //returns cards left
        return handList.Count;
    }

    private List<int> findPlayableCards(Card discardCard)
    {
        List<int> playableCards = new List<int>();
        for (int i = 0; i < handList.Count; i++)
        {
            Card card = handList[i];
            if (card.canBePlayedOn(discardCard))
            {
                playableCards.Add(i);
            }
        }
        return playableCards;
    }

    private List<Card> loseTwoCards(int currentIndex)
    {
        List<Card> discardCards = new List<Card>();
        System.Random random = new System.Random();
        for (int i = 0; i < 2; i++)
        {
            int randomIndex = currentIndex;
            while (randomIndex == currentIndex && handList.Count > 1)
            {
                randomIndex = random.Next(handList.Count());
            }
            if (randomIndex != currentIndex)
            {
                discardCards.Add(handList[randomIndex]);
                handList.RemoveAt(randomIndex);
            }
        }
        return discardCards;
    }
}
