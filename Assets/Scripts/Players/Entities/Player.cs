using Assets.Scripts.Players.Enums;
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
    public class Player : IPlayer
    {
        private const int DRAW_CARD_ONE = 1;
        private const string INVALID_COLOUR_ERROR = "Card colour {0} is an invalid card! Please play a card with this colour: {1}";
        private const string INVALID_NUMBER_ERROR = "Card number {0} is an invalid card! Please play a card with a value of +1 or -1 of the last card value, which is {1}!";

        private List<string> __Errors;

        public bool CanPlayCard(ICard playedCard, ICard shownCard)
        {
            bool _IsShownCardSpecial = Card.SpecialCards.Any(specialCard => specialCard == shownCard.TypeOfCard);
            bool _IsColourValid = playedCard.Colour == shownCard.Colour;

            //If see through
            if (playedCard.Colour == CardColour.SeeThrough)
            {
                return true;
            }

            switch (playedCard.TypeOfCard)
            {
                case CardType.Zero:
                case CardType.One:
                case CardType.Two:
                case CardType.Three:
                case CardType.Four:
                case CardType.Five:
                case CardType.Six:
                case CardType.Seven:
                case CardType.Eight:
                case CardType.Nine:
                    //If previous wrong colour special
                    if (!_IsColourValid && _IsShownCardSpecial)
                    {
                        Errors.Add(string.Format(INVALID_COLOUR_ERROR, playedCard.Colour.ToString(), shownCard.Colour.ToString()));
                        return false;
                    }
                    //If previous right colour reset
                    if (_IsColourValid && shownCard.TypeOfCard == CardType.Reset)
                    {
                        return true;
                    }

                    //Wrap around 0-9 (10-9)
                    int _CardMinusOne = playedCard.CardScore - 1;
                    if (_CardMinusOne == -1)
                    {
                        _CardMinusOne = 9;
                    }
                    //Wrap around 9-0 (10-1)
                    int _CardPlusOne = playedCard.CardScore + 1;
                    if (_CardPlusOne == 10)
                    {
                        _CardPlusOne = 0;
                    }

                    //If previous number card not 1 above or below
                    if (shownCard.CardScore != _CardMinusOne
                        && shownCard.CardScore != _CardPlusOne
                        && !_IsShownCardSpecial)
                    {
                        Errors.Add(string.Format(INVALID_NUMBER_ERROR, playedCard.CardScore.ToString(), shownCard.CardScore.ToString()));
                        return false;
                    }
                    //Otherwise ok
                    return true;
                case CardType.LoseTwo:
                case CardType.SwapDeck:
                case CardType.Reset:
                    //If previous wrong colour and different type of card
                    if (!_IsColourValid && playedCard.TypeOfCard != shownCard.TypeOfCard)
                    {
                        Errors.Add(string.Format(INVALID_COLOUR_ERROR, playedCard.Colour.ToString(), shownCard.Colour.ToString()));
                        return false;
                    }
                    return true;
            }

            return false;
        }

        public IDeck DrawCard(IDeck currentDeck)
        {
            Cards.Add(currentDeck.DrawCards(DRAW_CARD_ONE).SingleOrDefault());
            return currentDeck;
        }

        public IDeck LoseTwoCards(IDeck currentDeck)
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
            SpecialCardPlayed = SpecialCardPlayed.None;
            return currentDeck;
        }

        public IDeck PlayCard(ICard playedCard, IDeck currentDeck)
        {
            ICard _ShownCard = currentDeck.LastCardPlayed;
            //When comparing compare to most recent non see through  card. If none before - play any
            //Set shown card to last non-see through card. Otherwise keep as last played see through (i.e. only see through have been played)
            foreach (ICard _Card in currentDeck.PlayedCards)
            {
                if (_Card.TypeOfCard != CardType.SeeThrough)
                {
                    _ShownCard = _Card;
                }
            }

            if (CanPlayCard(playedCard, _ShownCard))
            {
                //Remove card from hand
                Cards.Remove(playedCard);
                //Add card to played
                currentDeck.PlayedCards.Add(playedCard);
                //Action of played card
                switch (playedCard.TypeOfCard)
                {
                    case CardType.SeeThrough:
                        //playedCard.Colour = _ShownCard.Colour;
                        //Instead just play see through card. When comparing compare to most recent non see through  card. If none before - play any
                        break;
                    case CardType.LoseTwo:
                        SpecialCardPlayed = SpecialCardPlayed.LoseTwo;
                        //remove 2 random cards from players hand - not the played card!!!
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
            return currentDeck;
        }

        public KeyValuePair<List<ICard>, List<ICard>> SwapCards(KeyValuePair<List<ICard>, List<ICard>> unswappedCards)
        {
            KeyValuePair<List<ICard>, List<ICard>> _SwappedCards = new KeyValuePair<List<ICard>, List<ICard>>(unswappedCards.Value, unswappedCards.Key);
            SpecialCardPlayed = SpecialCardPlayed.None;
            return _SwappedCards;
        }

        public List<string> ViewCards()
        {
            List<string> _CardToString = new List<string>();
            Cards.ForEach(card => _CardToString.Add(card.ToString()));
            return _CardToString;
        }

        public List<ICard> Cards { get; set; }
        public List<string> Errors => __Errors = __Errors ?? new List<string>();
        public string PlayerName { get; set; }
        public SpecialCardPlayed SpecialCardPlayed { get; set; }
    }
}
