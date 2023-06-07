using System.Collections.Generic;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;

namespace UnoDos.Players.Interfaces
{
    public interface ICPU : IPlayer
    {
        //bool HasCPUPlayedCard { get; }
        List<ICard> PlayableCards { get; }

        IDeck PlayCardCPU(IDeck currentDeck);

        List<ICard> PossibleCards(ICard shownCard);
    }
}