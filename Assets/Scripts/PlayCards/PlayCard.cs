using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;
using UnoDos.Players.Entities;
using UnoDos.Players.Interfaces;

public class PlayCard : MonoBehaviour
{

    public IDeck __Deck;
    public string __PlayedCardString;
    public IPlayer __Player;

    public PlayCard(IDeck deck, string playedCardString, IPlayer player)
    {
        __Deck = deck;
        __PlayedCardString = playedCardString;
        __Player = player;
    }

    public IDeck PlayerPlaysCard()
    {
        ICard _PlayedCard = __Player.Cards.SingleOrDefault(card => card.ToString() == __PlayedCardString);
        if (_PlayedCard != null)
        {
            __Deck = __Player.PlayCard(_PlayedCard, __Deck);
        }
        return __Deck;
    }
}
