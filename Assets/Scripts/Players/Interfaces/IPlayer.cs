﻿using Assets.Scripts.Players.Enums;
using System.Collections.Generic;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;

namespace UnoDos.Players.Interfaces
{
    public interface IPlayer
    {
        List<ICard> Cards { get; set; }
        List<string> Errors { get; }
        SpecialCardPlayed SpecialCardPlayed { get; set; }
        string PlayerName { get; set; }

        bool CanPlayCard(ICard playedCard, ICard shownCard);
        IDeck DrawCard(IDeck currentDeck);
        IDeck LoseTwoCards(IDeck currentDeck);
        IDeck PlayCard(ICard playedCard, IDeck currentDeck);
        KeyValuePair<List<ICard>, List<ICard>> SwapCards(KeyValuePair<List<ICard>, List<ICard>> unswappedCards);
        List<string> ViewCards();
    }
}