using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck
{

    private List<Card> deck;

    public Deck(GameObject regCardPrefab, GameObject loseTwoPrefab, GameObject resetCardPrefab, GameObject swapDeckPrefab, GameObject seeThroughPrefab)
    {
        this.deck = new List<Card>();
        createDeck(regCardPrefab, loseTwoPrefab, resetCardPrefab, swapDeckPrefab, seeThroughPrefab);
        shuffleDeck();
    }

    private void createDeck(GameObject regCardPrefab, GameObject loseTwoPrefab, GameObject resetCardPrefab, GameObject swapDeckPrefab, GameObject seeThroughPrefab)
    {
        // Creates Deck
        // 4 x 0
        // 8 x 1-9
        // 8 x Lose Two
        // 8 x Reset
        // 8 x Swap Deck
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                switch (i)
                {
                    case 0:
                        addCardToDeck(i, j, regCardPrefab);
                        break;
                    case 10:
                        addCardToDeck(i, j, loseTwoPrefab);
                        addCardToDeck(i, j, loseTwoPrefab);
                        break;
                    case 11:
                        addCardToDeck(i, j, resetCardPrefab);
                        addCardToDeck(i, j, resetCardPrefab);
                        break;
                    case 12:
                        addCardToDeck(i, j, swapDeckPrefab);
                        addCardToDeck(i, j, swapDeckPrefab);
                        break;
                    default:
                        addCardToDeck(i, j, regCardPrefab);
                        addCardToDeck(i, j, regCardPrefab);
                        break;
                }
            }
        }

        // 5 x See Through
        for (int i = 0; i < 5; i++)
        {
            addCardToDeck(13, 4, seeThroughPrefab);
        }

    }

    // Method to create card and add to deck
    private void addCardToDeck(int value, int colourValue, GameObject prefab)
    {
        deck.Add(new Card(value, returnColourName(colourValue), prefab));
    }

    // Method to return a colour from a number value
    private string returnColourName(int numb)
    {
        switch (numb)
        {
            case 0:
                return "Pink";
            case 1:
                return "Green";
            case 2:
                return "Orange";
            case 3:
                return "Purple";
            case 4:
                return "Black";
        }
        return "";
    }

    // Method to shuffle deck
    public void shuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = cardAtIdx(i);
            int posSwitch = Random.Range(0, deck.Count);
            deck[i] = deck[posSwitch];
            deck[posSwitch] = temp;
        }
    }

    public void resetDeck(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            if (card.getNumb() == 13)
            {
                card.changeColor("Black");
            }
            addCard(card);
        }
        shuffleDeck();
    }

    // Accessor Methods

    public List<Card> getDeck()
    {
        return deck;
    }

    public void addCard(Card card)
    {
        deck.Add(card);
    }

    public void addCards(List<Card> cards)
    {
        cards.ForEach(card => addCard(card));
    }

    public void removeAtIdx(int idx)
    {
        deck.RemoveAt(idx);
    }

    public Card cardAtIdx(int idx)
    {
        return deck[idx];
    }

    public int size()
    {
        return deck.Count;
    }
}