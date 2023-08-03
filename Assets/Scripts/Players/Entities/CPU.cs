using Assets.Scripts.Players.Difficulty;
using Assets.Scripts.Players.Difficulty.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnoDos.Cards.Entities;
using UnoDos.Cards.Enums;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;
using UnoDos.Players.Interfaces;

namespace UnoDos.Players.Entities
{
    public class CPU : Player, ICPU
    {
        private IPlayer __Player;

        public CPU(IPlayer player)
        {
            __Player = player;
        }

        private ICard CalculateBestMove(List<ICard> playableCards, ICard lastCardPlayed, int bestCardToPlayIndex = 0)
        {
            bool _PlayCard = true;
            List<ICard> _OrderedPlayableCard = playableCards.OrderByDescending(card => card.CardScore).ToList();
            ICard _BestCardToPlay = _OrderedPlayableCard[bestCardToPlayIndex];
            if (_BestCardToPlay.TypeOfCard == CardType.SwapDeck || _BestCardToPlay.TypeOfCard == CardType.SeeThrough)
            {
                _PlayCard = ShouldPlaySwapDeckOrSeeThroughCard(playableCards, _BestCardToPlay, lastCardPlayed);
            }
            if (_PlayCard)
            {
                return _BestCardToPlay;
            }
            else
            {
                return CalculateBestMove(playableCards, lastCardPlayed, bestCardToPlayIndex++);
            }

        }

        private IDeck LoseTwoCards(IDeck currentDeck)
        {
            if (Cards.Count < 2)
            {
                currentDeck.DeckOfCards.Add(Cards[0]);
                Cards = new List<ICard>();
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    Random _Random = new Random();
                    int _SelectedRandomIndex = _Random.Next(Cards.Count());
                    ICard _CardToLose = Cards[_SelectedRandomIndex];
                    Cards.Remove(_CardToLose);
                    currentDeck.DeckOfCards.Add(_CardToLose);
                }
            }
            return currentDeck;
        }

        //Method for CPU to play a card after the list of playable cards has been generated
        public IDeck PlayCardCPU(IDeck currentDeck)
        {
            ICard _ShownCard = currentDeck.LastCardPlayed;

            //List of possible CPU cards
            PlayableCards = PossibleCards(_ShownCard);
            //If can play
            if (PlayableCards.Count > 0)
            {
                //Select a random playable card
                DifficultyRolls __Roll = new DifficultyRolls(CPUDifficulty);
                bool _RollToNotDrawCard = __Roll.Roll();
                if (!_RollToNotDrawCard)
                {
                    DrawCard(currentDeck);
                }
                else
                {
                    bool _RollToPlayBestCard = __Roll.Roll();
                    ICard _PlayedCard = CalculateBestMove(PlayableCards, currentDeck.LastCardPlayed);
                    if (!_RollToPlayBestCard)
                    {
                        PlayableCards.Remove(_PlayedCard);
                        Random _RandomCardToPlayGenerator = new Random();
                        _PlayedCard = PlayableCards[_RandomCardToPlayGenerator.Next(PlayableCards.Count)];
                    }
                    if (_PlayedCard.TypeOfCard == CardType.SeeThrough)
                    {
                        _PlayedCard.Colour = _ShownCard.Colour;
                    }
                    //Remove card from hand
                    Cards.Remove(_PlayedCard);
                    //Add card to played
                    currentDeck.PlayedCards.Add(_PlayedCard);
                    //Action of played card
                    if (_PlayedCard.TypeOfCard == CardType.LoseTwo)
                    {
                        currentDeck = LoseTwoCards(currentDeck);
                    }
                }
            }
            //Can't play - pick up
            else
            {
                DrawCard(currentDeck);
            }

            return currentDeck;
        }

        //Method to generate a list of the cards the CPU could play
        public List<ICard> PossibleCards(ICard shownCard)
        {
            PlayableCards = new List<ICard>();
            //For each card the CPU has
            foreach (ICard _Card in Cards)
            {
                //Calls the parent Player().CanPLayCard() method
                if (CanPlayCard(_Card, shownCard))
                {
                    //And adds card to list if playable
                    PlayableCards.Add(_Card);
                }
            }
            return PlayableCards;
        }

        private bool ShouldPlaySwapDeckOrSeeThroughCard(List<ICard> playableCards, ICard desiredCardToPlay, ICard lastShownCard)
        {
            bool _SameColourAsLastShown = playableCards.Any(card => card.Colour == lastShownCard.Colour);
            if (desiredCardToPlay.TypeOfCard == CardType.SeeThrough)
            {
                return !_SameColourAsLastShown;
            }
            if (desiredCardToPlay.TypeOfCard == CardType.SwapDeck)
            {
                return !_SameColourAsLastShown || __Player.Cards.Count() < Cards.Count();
            }
            return false;
        }

        public DifficultyLevel CPUDifficulty { get; set; }
        public List<ICard> PlayableCards { get; private set; }
        public IPlayer Player
        {
            get => __Player;
            set => __Player = value;
        }
    }
}