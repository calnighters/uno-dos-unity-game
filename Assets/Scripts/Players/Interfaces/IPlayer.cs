using System.Collections.Generic;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;

namespace UnoDos.Players.Interfaces
{
    public interface IPlayer
    {
        bool CanPlayCard(ICard playedCard, ICard shownCard);
        IDeck DrawCard(IDeck currentDeck);
        IDeck LoseTwoCards(IDeck currentDeck, ICard playedCard);
        IDeck PlayCard(ICard playedCard, IDeck currentDeck);
        List<string> ViewCards();
        int CalculateScore();

        List<ICard> Cards { get; set; }
        List<string> Errors { get; }
        bool HasPlayerPlayedCard { get; set; }
        bool IsHPMode { get; set; }
        int LoseTwoCardCount { get; set; }
        string PlayerName { get; set; }
        int PlayerScore { get; set; }
        int RemainingHP { get; set; }
    }
}