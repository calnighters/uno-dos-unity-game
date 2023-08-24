using Assets.Scripts.Players.Difficulty.Enums;
using System.Collections.Generic;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;

namespace UnoDos.Players.Interfaces
{
    public interface ICPU : IPlayer
    {
        IDeck PlayCardCPU(IDeck currentDeck);

        List<ICard> PossibleCards(ICard shownCard);

        int CPUPlayerNumber { get; set; }
        DifficultyLevel CPUDifficulty { get; set; }
        List<ICard> PlayableCards { get; }
        IPlayer Player { get; set; }
        bool HasCPUPlayedCard { get; set; }
    }
}