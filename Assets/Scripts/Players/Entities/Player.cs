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
        private const int OUT_OF_HP_PENALTY = 150;

        public Player()
        {
            LoseTwoCardCount = 0;
            HasPlayerPlayedCard = false;
        }

        private List<string> __Errors;

        public bool CanPlayCard(ICard playedCard, ICard shownCard)
        {
            //bool _IsShownCardST = shownCard.TypeOfCard == CardType.SeeThrough
            bool _IsShownCardSpecial = Card.SpecialCards.Any(specialCard => specialCard == shownCard.TypeOfCard);
            bool _IsColourValid = playedCard.Colour == shownCard.Colour;

            //If see through
            if (playedCard.TypeOfCard == CardType.SeeThrough)
            {
                //If same colour
                if (_IsColourValid)
                {
                    return true;
                }
                return false;
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
                case CardType.SeeThrough:
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
            if(IsHPMode)
            {
                RemainingHP -= 1;
            }
            return currentDeck;
        }

        public IDeck LoseTwoCards(IDeck currentDeck, ICard playedCard)
        {
            Cards.Remove(playedCard);
            currentDeck.DeckOfCards.Add(playedCard);
            LoseTwoCardCount++;

            return currentDeck;
        }

        public IDeck PlayCard(ICard playedCard, IDeck currentDeck)
        {
            ICard _ShownCard = currentDeck.getLastNonSTCard();

            if (CanPlayCard(playedCard, _ShownCard))
            {
                if(playedCard.TypeOfCard == CardType.SeeThrough)
                {
                    playedCard.Colour = _ShownCard.Colour;
                }
                //Remove card from hand
                Cards.Remove(playedCard);
                //Add card to played
                currentDeck.PlayedCards.Add(playedCard);
                HasPlayerPlayedCard = true;
            }
            else
            {
                HasPlayerPlayedCard = false;
            }
            
            return currentDeck;
        }

        public List<string> ViewCards()
        {
            List<string> _CardToString = new List<string>();
            Cards.ForEach(card => _CardToString.Add(card.ToString()));
            return _CardToString;
        }

        public int CalculateScore()
        {
            foreach (Card card in Cards)
            {
                PlayerScore += card.CardScore;
            }
            if(RemainingHP <= 0)
            {
                PlayerScore += OUT_OF_HP_PENALTY;
            }
            return PlayerScore;
        }

        public List<ICard> Cards { get; set; }
        public List<string> Errors => __Errors = __Errors ?? new List<string>();
        public bool HasPlayerPlayedCard { get; set; }
        public bool IsHPMode { get; set; }
        public int LoseTwoCardCount { get; set; }
        public string PlayerName { get; set; }
        public int PlayerScore { get; set; }
        public int RemainingHP { get; set; }
    }
}
