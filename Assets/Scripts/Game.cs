using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Game : MonoBehaviour
{

    List<PlayerInterface> players;
    List<Card> discardPile;
    Deck deck;
    int playerIndex;

    public Game(List<PlayerInterface> players, List<Card> discardPile, Deck deck)
    {
        this.players = players;
        this.discardPile = discardPile;
        this.deck = deck;
        this.playerIndex = 0;
    }

    // Method to control the turn taking of a player
    public void takeTurn()
    {
        Control control = GameObject.Find("Control").GetComponent<Control>();
        GameObject deckGO = control.deckGO;

        // If human player, add listener to draw card
        if (players[playerIndex] is HumanPlayer)
        {
            HumanPlayer player = (HumanPlayer)players[playerIndex];
            deckGO.GetComponent<Button>().onClick.RemoveAllListeners();
            deckGO.GetComponent<Button>().onClick.AddListener(() =>
            {
                drawCard(1, player);
                player.recieveDrawOnTurn();
            });
        }

        // If player present then take turn and update discard pile object
        if (players[playerIndex] != null)
        {
            // Initiate players turn
            players[playerIndex].turn(discardPile);
        }

        // Move on to next player
        playerIndex += 1;
        if (playerIndex >= players.Count)
        {
            playerIndex = 0;
        }
    }

    public void updateDiscardPile(Card card)
    {
        Control control = GameObject.Find("Control").GetComponent<Control>();

        Destroy(control.discardPileObject);
        discardPile.Add(card);
        control.discardPileObject = discardPile[discardPile.Count - 1].loadCard(725, -325, GameObject.Find("Main").transform);
        control.discardPileObject.transform.SetSiblingIndex(9);
    }

    // Method to draw cards for a player
    public void drawCard(int amount, PlayerInterface player)
    {
        if (deck.size() < amount)
        {
            resetDeck();
        }
        for (int i = 0; i < amount; i++)
        {
            player.addCards(deck.cardAtIdx(0));
            deck.removeAtIdx(0);
        }
    }

    // Method to reset the deck of cards using the discard pile
    private void resetDeck()
    {
        // Get last card placed
        Card last = discardPile[discardPile.Count - 1];
        // Remove from discard pile
        discardPile.RemoveAt(discardPile.Count - 1);
        // Replenish deck with remaining discard pile
        deck.resetDeck(discardPile);
        // Clear discard pile
        discardPile.Clear();
        // Reset with last card played
        discardPile.Add(last);
    }

    public void addToBottomOfDeck(List<Card> cards)
    {
        deck.addCards(cards);
    }
}