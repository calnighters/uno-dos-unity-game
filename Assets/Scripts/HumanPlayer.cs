using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanPlayer : MonoBehaviour, PlayerInterface
{

    bool drew = false;
    string name;
    List<Card> handList = new List<Card>();

    public HumanPlayer(string name)
    {
        this.name = name;
    }

    // Initiate human player turn
    public void turn(List<Card> discardPile)
    {
        Control control = GameObject.Find("Control").GetComponent<Control>();
        GameObject playerHand = control.playerHand;

        int i = 0;
        foreach (Card card in handList)
        {
            GameObject cardObj = null;
            // Does the card need to be loaded
            if (playerHand.transform.childCount > i)
            {
                cardObj = playerHand.transform.GetChild(i).gameObject;
            }
            else
            {
                cardObj = card.loadCard(playerHand.transform);
            }

            if (handList[i].canBePlayedOn(discardPile[discardPile.Count - 1]))
            {
                setListeners(i, cardObj);
            }
            else
            {
                cardObj.transform.GetChild(3).gameObject.SetActive(true); //otherwise black them out
            }
            i++;
        }
    }

    // Set click listeners to select card
    public void setListeners(int handIndex, GameObject cardObj)
    {
        cardObj.GetComponent<Button>().onClick.AddListener(() =>
        {
            cardObj.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(cardObj);
            turnEnd(handIndex);
        });
    }

    // Add card to players hand
    public void addCards(Card card)
    {
        handList.Add(card);
    }

    // Handle a turn where the player drew a card
    public void recieveDrawOnTurn()
    {
        handList[handList.Count - 1].loadCard(GameObject.Find("Control").GetComponent<Control>().playerHand.transform);
        drew = true;
        turnEnd(-1);
    }

    // Handle the user selecting a card to turn
    public void turnEnd(int cardIndex)
    {
        Control control = GameObject.Find("Control").GetComponent<Control>();
        GameObject playerHand = control.playerHand;

        playerHand.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

        for (int i = playerHand.transform.childCount - 1; i >= 0; i--)
        {
            playerHand.transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
            playerHand.transform.GetChild(i).GetChild(3).gameObject.SetActive(false);
        }
        if (drew)
        {
            control.GetComponent<Control>().enabled = true;
            control.updateLog(string.Format("{0} drew a card", name));
            control.deckGO.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        else
        {
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
                    control.loseTwoCards(null);
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

    }
    public bool Equals(PlayerInterface other)
    { //equals function based on name
        return other.getName().Equals(name);
    }
    public string getName()
    { //returns the name
        return name;
    }
    public int getCardsLeft()
    { //gets how many cards are left in the hand
        return handList.Count;
    }
}
