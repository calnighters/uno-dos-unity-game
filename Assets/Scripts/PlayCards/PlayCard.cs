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
    public string __PlayedCardString;
    public IPlayer __Player;
    public ICPU __CPU;

    public PlayCard(IDeck deck, string playedCardString, IPlayer player, ICPU cpu)
    {
        __Deck = deck;
        __PlayedCardString = playedCardString;
        __Player = player;
        __CPU = cpu;
    }

    public PlayCard(IDeck deck, ICPU cpu, IPlayer player)
    {
        __Deck = deck;
        __CPU=cpu;
        __Player = player;
        __PlayedCardString = null;
    }

    public IDeck PlayerPlaysCard()
    {
        ICard _PlayedCard = __Player.Cards.SingleOrDefault(card => card.ToString() == __PlayedCardString);
        ICard _ShownCard = __Deck.LastCardPlayed;
        if (_PlayedCard != null)
        {   
            //Don't swap if Player can't play
            if (__Player.CanPlayCard(_PlayedCard, _ShownCard))
            {
                __Deck = __Player.PlayCard(_PlayedCard, __Deck);
                //Functionality for swapping decks between player and CPU. Easier in this class than player / CPU classes
                if (_PlayedCard.TypeOfCard == CardType.SwapDeck)
                {
                    swapDecks();
                }
            }
            //Player can't play
            else
            {
                __Deck = __Player.PlayCard(_PlayedCard, __Deck);
            }
        }
        return __Deck;
    }

    public IDeck CPUPlaysCard()
    {
        //ICard _PlayedCard = __Player.Cards.SingleOrDefault(card => card.ToString() == __PlayedCardString);
        ICard _ShownCard = __Deck.LastCardPlayed;
        
        //If CPU can play
        if (__CPU.PossibleCards(_ShownCard).Count > 0)
        {
            __Deck = __CPU.PlayCardCPU(__Deck);
            //Functionality for swapping decks between player and CPU. Easier in this class than player / CPU classes
            if (__Deck.LastCardPlayed.TypeOfCard == CardType.SwapDeck)
            {
                swapDecks();
            }
        }
        //CPU can't play
        else
        {
            //Draw card
            __Deck = __CPU.PlayCardCPU(__Deck);
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
