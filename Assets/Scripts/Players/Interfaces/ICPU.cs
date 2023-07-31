using System.Collections.Generic;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;

namespace UnoDos.Players.Interfaces
{
    public interface ICPU : IPlayer
    {
        IDeck PlayCardCPU(IDeck currentDeck);

        List<ICard> PossibleCards(ICard shownCard);

        List<ICard> PlayableCards { get; }
    }
}