using System.Collections.Generic;
using UnityEngine;
using UnoDos.Cards.Enums;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;
using UnoDos.Players.Interfaces;

public class PlayCard : MonoBehaviour
{
    private ICPU __CPU;
    private IDeck __Deck;
    private IPlayer __Player;

    public PlayCard(IDeck deck, ICPU cpu, IPlayer player)
    {
        __Deck = deck;
        __Player = player;
        __CPU = cpu;
        IsPlayerTurn = true;
    }

    public IDeck CPUPlaysCard()
    {
        if (!IsPlayerTurn)
        {
            __Deck = __CPU.PlayCardCPU(__Deck);
        }

        //Functionality for swapping decks between player and CPU. Easier in this class than player / CPU classes
        if (__Deck.LastCardPlayed.TypeOfCard == CardType.SwapDeck)
        {
            SwapDecks();
        }
        IsPlayerTurn = __Deck.LastCardPlayed.TypeOfCard == CardType.Reset ? false : true;
        return __Deck;
    }

    public IDeck PlayerPlaysCard()
    {
        if (IsPlayerTurn)
        {

            if(__Deck.LastCardPlayed.TypeOfCard == CardType.LoseTwo && __Player.HasPlayerPlayedCard)
            {
                PlayerPlaysLoseTwo();
            }
            else
            {
                __Deck = __Player.PlayCard(PlayedCard, __Deck);
                if (__Player.HasPlayerPlayedCard)
                {
                    //Functionality for swapping decks between player and CPU. Easier in this class than player / CPU classes
                    if (__Deck.LastCardPlayed.TypeOfCard == CardType.SwapDeck)
                    {
                        SwapDecks();
                    }
                    IsPlayerTurn = __Deck.LastCardPlayed.TypeOfCard == CardType.Reset || __Deck.LastCardPlayed.TypeOfCard == CardType.LoseTwo ? true : false;
                    __Player.HasPlayerPlayedCard = IsPlayerTurn;
                }
            }
        }
        return __Deck;
    }

    private void PlayerPlaysLoseTwo()
    {
        __Deck = __Player.LoseTwoCards(__Deck, PlayedCard);
        IsPlayerTurn = true;
        if(__Player.LoseTwoCardCount == 2)
        {
            __Player.LoseTwoCardCount = 0;
            IsPlayerTurn = false;
        }
    }

    private void SwapDecks()
    {
        List<ICard> _TemporaryPlayerCards = __Player.Cards;
        __Player.Cards = __CPU.Cards;
        __CPU.Cards = _TemporaryPlayerCards;
    }

    public ICPU CPU => __CPU;
    public bool IsPlayerTurn { get; set; }
    public IPlayer Player => __Player;

    public ICard PlayedCard { get; set; }
}
