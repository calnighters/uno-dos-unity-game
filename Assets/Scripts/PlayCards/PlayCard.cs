using System.Collections.Generic;
using UnityEngine;
using UnoDos.Cards.Enums;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;
using UnoDos.Players.Interfaces;

public class PlayCard : MonoBehaviour
{
    public ICPU __CPU;
    public IDeck __Deck;
    public ICard __PlayedCard;
    public IPlayer __Player;

    public PlayCard(IDeck deck, ICPU cpu, IPlayer player)
    {
        __Deck = deck;
        __PlayedCard = null;
        __Player = player;
        __CPU =cpu;
    }

    public PlayCard(IDeck deck, ICard playedCard, IPlayer player, ICPU cpu)
    {
        __Deck = deck;
        __PlayedCard = playedCard;
        __Player = player;
        __CPU = cpu;
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
                SwapDecks();
            }
        }
        return __Deck;
    }

    public IDeck PlayerPlaysCard()
    {
        __Deck = __Player.PlayCard(__PlayedCard, __Deck);
        if (__PlayedCard != null)
        {   
            //Don't swap if Player can't play
            if (__PlayedCard == __Deck.LastCardPlayed)
            {
                //Functionality for swapping decks between player and CPU. Easier in this class than player / CPU classes
                if (__PlayedCard.TypeOfCard == CardType.SwapDeck)
                {
                    SwapDecks();
                }
            }
        }
        return __Deck;
    }

    private void SwapDecks()
    {
        List<ICard> _TemporaryPlayerCards = __Player.Cards;
        __Player.Cards = __CPU.Cards;
        __CPU.Cards = _TemporaryPlayerCards;
    }

    public ICPU CPU => __CPU;
    public IPlayer Player => __Player;
}
