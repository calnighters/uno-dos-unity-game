using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnoDos.Cards.Enums;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Entities;
using UnoDos.Decks.Interfaces;
using UnoDos.Players.Entities;
using UnoDos.Players.Interfaces;

public class PlayCard : MonoBehaviour
{

    public IDeck __Deck;
    public ICard __PlayedCard;
    public IPlayer __Player;
    public ICPU __CPU;

    public PlayCard(IDeck deck, ICard playedCard, IPlayer player, ICPU cpu)
    {
        __Deck = deck;
        __PlayedCard = playedCard;
        __Player = player;
        __CPU = cpu;
    }

    public PlayCard(IDeck deck, ICPU cpu, IPlayer player)
    {
        __Deck = deck;
        __PlayedCard = null;
        __Player = player;
        __CPU =cpu;
    }

    public IDeck PlayerPlaysCard()
    {
        ICard _ShownCard = __Deck.LastCardPlayed;

        __Deck = __Player.PlayCard(__PlayedCard, __Deck);
        if (__PlayedCard != null)
        {   
            //Don't swap if Player can't play
            if (__PlayedCard == __Deck.LastCardPlayed)
            {
                //Functionality for swapping decks between player and CPU. Easier in this class than player / CPU classes
                if (__PlayedCard.TypeOfCard == CardType.SwapDeck)
                {
                    swapDecks();
                }
                print("Player played " + __PlayedCard.ToString());
            }
            //Player can't play
            else
            {
                print("Player unable to play " + __PlayedCard.ToString());
            }
        }
        else
        {
            print("Player card null");
        }
        return __Deck;
    }

    public IDeck CPUPlaysCard()
    {
        //ICard _PlayedCard = __Player.Cards.SingleOrDefault(card => card.ToString() == __PlayedCardString);
        ICard _ShownCard = __Deck.LastCardPlayed;
        __Deck = __CPU.PlayCardCPU(__Deck);

        //If CPU can play
        if (__Deck.LastCardPlayed != _ShownCard)
        {
            //Functionality for swapping decks between player and CPU. Easier in this class than player / CPU classes
            if (__Deck.LastCardPlayed.TypeOfCard == CardType.SwapDeck)
            {
                //Bug here - Are player cards not updated??
                swapDecks();
            }
            print("CPU played " + __Deck.LastCardPlayed.ToString());
        }
        //CPU can't play
        else
        {
            //Draw card
            print("CPU picked up");
        }
        return __Deck;
    }

    public IPlayer getPlayer()
    {
        return __Player;
    }

    public ICPU getCPU()
    {
        return __CPU;
    }

    private void swapDecks()
    {
        List<ICard> tempPlayerCards = __Player.Cards;
        __Player.Cards = __CPU.Cards;
        __CPU.Cards = tempPlayerCards;
    }
}
