using Assets.Scripts.Players.Enums;
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
        

        //Method for CPU to play a card after the list of playable cards has been generated
        public IDeck PlayCardCPU(IDeck currentDeck)
        {
            ICard _ShownCard = currentDeck.LastCardPlayed;
            //List of possible CPU cards
            PlayableCards = PossibleCards(currentDeck.LastCardPlayed);
            //If can play
            if (PlayableCards.Count > 0)
            {
                //Select a random playable card
                Random _Random = new Random();
                int _Index1 = _Random.Next(PlayableCards.Count());
                ICard _PlayedCard = PlayableCards[_Index1];
                //Remove card from hand
                Cards.Remove(_PlayedCard);
                //Add card to played
                currentDeck.PlayedCards.Add(_PlayedCard);
                //Action of played card
                switch (_PlayedCard.TypeOfCard)
                {
                    case CardType.SeeThrough:
                        //Set see through 
                        _PlayedCard.Colour = _ShownCard.Colour;
                        break;
                    case CardType.LoseTwo:
                        SpecialCardPlayed = SpecialCardPlayed.LoseTwo;
                        //remove 2 random cards from CPU hand - not the played card!!!
                        currentDeck = LoseTwoCards(currentDeck);
                        break;
                    case CardType.SwapDeck:
                        SpecialCardPlayed = SpecialCardPlayed.SwapDeck;
                        //swap player and cpu cards - done in PlayCard class
                        break;
                    case CardType.Reset:
                        SpecialCardPlayed = SpecialCardPlayed.Reset;
                        //next card can be any card - no action - check made on next played card
                        break;
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

        //public bool HasCPUPlayedCard { get; private set; }
        public List<ICard> PlayableCards { get; private set; }
    }
}