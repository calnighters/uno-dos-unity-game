using System;
using System.Collections.Generic;
using System.Linq;
using UnoDos.Cards.Entities;
using UnoDos.Cards.Enums;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;

namespace UnoDos.Decks.Entities
{
    public class Deck : IDeck
    {
        private const int DRAW_FACE_CARD_AMOUNT = 1;
        private const int LOSE_TWO_CARD_VALUE = 50;
        private const int RESET_CARD_VALUE = 40;
        private const int SEE_THROUGH_CARD_VALUE = 30;
        private const int SWAP_DECK_CARD_VALUE = 20;

        private ICard __CardCreator;
        private List<ICard> __DeckOfCards;
        private List<ICard> __PlayedCards;
        private ICard __LastNonSTCard;

        private void AddColourCards(CardColour colour)
        {
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.Zero, Convert.ToInt32(CardType.Zero)));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.One, Convert.ToInt32(CardType.One)));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.Two, Convert.ToInt32(CardType.Two)));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.Three, Convert.ToInt32(CardType.Three)));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.Four, Convert.ToInt32(CardType.Four)));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.Five, Convert.ToInt32(CardType.Five)));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.Six, Convert.ToInt32(CardType.Six)));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.Seven, Convert.ToInt32(CardType.Seven)));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.Eight, Convert.ToInt32(CardType.Eight)));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.Nine, Convert.ToInt32(CardType.Nine)));

            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.Reset, RESET_CARD_VALUE));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.LoseTwo, LOSE_TWO_CARD_VALUE));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.SwapDeck, SWAP_DECK_CARD_VALUE));
            DeckOfCards.Add(CardCreator.CreateCard(DeckOfCards.Count + 1, colour, CardType.SeeThrough, SEE_THROUGH_CARD_VALUE));
        }

        public void CreateDeck()
        {
            foreach (CardColour _Colour in Enum.GetValues(typeof(CardColour)))
            {
                switch (_Colour)
                {
                    case CardColour.Pink:
                    case CardColour.Green:
                    case CardColour.Orange:
                    case CardColour.Purple:
                        //Add 2 of each card
                        AddColourCards(_Colour);
                        AddColourCards(_Colour);
                        break;
                }
            }
        }

        public List<ICard> Deal(int numberOfCards)
        {
            if (numberOfCards <= 0)
            {
                throw new ArgumentOutOfRangeException("numberOfCards", "Number of cards must be greater than 0. ");
            }

            if (DeckOfCards.Count < numberOfCards)
            {
                throw new InvalidOperationException("Not enough cards in the deck to deal ");
            }

            List<ICard> dealtCards = DeckOfCards.Take(numberOfCards).ToList();
            DeckOfCards.RemoveRange(0, numberOfCards);

            return dealtCards;
        }

        // Draw Card Method
        public ICard DrawCard()
        {
            Reshuffle();
            ICard _DrawnCard = DeckOfCards.Take(DRAW_FACE_CARD_AMOUNT).SingleOrDefault();

            //Remove the drawn cards from the draw pile
            DeckOfCards.Remove(_DrawnCard);
            return _DrawnCard;
        }
       
        public List<ICard> DrawCards(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count", "count must be greater than 0");
            }

            if ((DeckOfCards.Count > 1 && DeckOfCards.Count < count) 
                || DeckOfCards.Count < 1 && PlayedCards.Count < 1)
            {
                throw new InvalidOperationException("Not enough cards in deck");
            }

            Reshuffle();

            List<ICard> _DrawnCards = DeckOfCards.Take(count).ToList();

            //Remove the drawn cards from the draw pile
            DeckOfCards.RemoveAll(card => _DrawnCards.Contains(card));
            return _DrawnCards;
        }

        public ICard DrawInitialCard()
        {
            ICard _DrawnCard = DeckOfCards.Where(card => !Card.SpecialCards.Contains(card.TypeOfCard)).Take(DRAW_FACE_CARD_AMOUNT).SingleOrDefault();

            //Remove the drawn cards from the draw pile
            PlayedCards.Add(_DrawnCard);
            DeckOfCards.Remove(_DrawnCard);
            return _DrawnCard;
        }

        private void Reshuffle()
        {
            if (DeckOfCards.Count < 1)
            {
                __DeckOfCards = PlayedCards;
                Shuffle();
                ICard _FaceCard = DrawCard();
                __PlayedCards = new List<ICard> { _FaceCard };
            }
        }

        // Shuffle Card Method
        public void Shuffle()
        {
            Random _Random = new Random();

            List<ICard> _Cards = DeckOfCards;

            for (int i = _Cards.Count - 1; i >= 0; --i)// may need to check this for condition
            {
                int j = _Random.Next(i + 1);
                ICard _TemporaryCard = _Cards[i];
                _Cards[i] = _Cards[j];
                _Cards[j] = _TemporaryCard;

            }
        }

        public void setLastNonSTCard()
        {
            bool _CardSet = false;
            foreach (ICard card in PlayedCards)
            {
                if (card.TypeOfCard != CardType.SeeThrough)
                {
                    __LastNonSTCard = card;
                    _CardSet = true;
                }
            }
            if (!_CardSet)
            {
                __LastNonSTCard = LastCardPlayed;
            }
        }

        public ICard getLastNonSTCard()
        {
            setLastNonSTCard();
            return __LastNonSTCard;
        }

        private ICard CardCreator => __CardCreator = __CardCreator ?? new Card();
        public List<ICard> DeckOfCards => __DeckOfCards = __DeckOfCards ?? new();
        public ICard LastCardPlayed => PlayedCards.Last();
        public List<ICard> PlayedCards => __PlayedCards = __PlayedCards ?? new();
        /*public ICard LastNonSTCard 
        { 
            get { return LastNonSTCard; }
            set { setLastNonSTCard(); }
        } */
    }
}
