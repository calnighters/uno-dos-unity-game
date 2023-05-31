using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoDos.Decks.Entities;
using UnoDos.Cards.Entities;
using UnoDos.Cards.Interfaces;
using UnoDos.Cards.Enums;

public class GamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck deck;

    public GameObject Card1;
    public GameObject PlayerArea;

    public GameObject OpponentArea;

    public GameObject deckButton;

    private List<ICard> playerHand;

    public List<ICard> cpuHand;
    public List<ICard> playedCards;

    void Start()
    {
        // Create a new instance of a Deck
        deck = new Deck();

        // Create the deck of cards
        deck.CreateDeck();

        // shuffle the deck
        deck.Shuffle();

        playerHand = new List<ICard>();
        cpuHand = new List<ICard>();

        // Deal cards to the player
        //DealCards();
    }

    public void DealCards()
    {

        int numberOfCardsToDeal = 10;

        List<ICard> playerDealtCards = deck.Deal(numberOfCardsToDeal);

        List<ICard> cpuDealtCards = deck.Deal(numberOfCardsToDeal);

        // Dealt Cards are added to the players hand
        playerHand.AddRange(playerDealtCards);

        //Dealt Cards are added to cpu hand
        cpuHand.AddRange(cpuDealtCards);

        foreach (ICard card in playerDealtCards)
        {
            GameObject newCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
            newCard.transform.SetParent(PlayerArea.transform, false);
            newCard.name = card.ToString();
            
        }
        
        foreach (ICard card in cpuDealtCards)
        {
            GameObject newCard2 = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
            newCard2.transform.SetParent(OpponentArea.transform, false);
            newCard2.name = card.ToString();
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
