using System;
using System.Collections.Generic;
using System.Linq;
using UnoDos.Cards.Enums;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;
using UnoDos.Players.Interfaces;

namespace UnoDos.Players.Entities
{
    public class CPU : Player, ICPU
    {
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
                Random _Random = new Random();
                int SelectedRandomCardIndex = _Random.Next(PlayableCards.Count());
                ICard _PlayedCard = PlayableCards[SelectedRandomCardIndex];
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

        public List<ICard> PlayableCards { get; private set; }
    }
}