using System.Collections.Generic;
using UnoDos.Cards.Interfaces;

namespace UnoDos.Decks.Interfaces
{
    public interface IDeck
    {
        void CreateDeck();
        List<ICard> Deal(int numberOfCards);
        ICard DrawCard();
        List<ICard> DrawCards(int count);
        ICard DrawInitialCard();
        void Shuffle();
        List<ICard> DeckOfCards { get; }
        ICard LastCardPlayed { get; }
        List<ICard> PlayedCards { get; }
        //ICard LastNonSTCard { get; set; }
        void setLastNonSTCard();
        ICard getLastNonSTCard();
    }
}