﻿using UnoDos.Cards.Interfaces;
using UnoDos.Cards.Enums;
using UnoDos.Extensions;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnoDos.Cards.Entities
{
    public class Card : ICard
    {
        public ICard CreateCard(int cardID, CardColour colour, CardType cardType, int cardScore)
        {
            return new Card { CardID = cardID, Colour = colour, TypeOfCard = cardType, CardScore = cardScore };
        }

        public override string ToString()
        {
            return Colour.GetDescriptionFromEnum() + " " + TypeOfCard.GetDescriptionFromEnum();
        }

        public int CardID { get; set; }
        public Image CardImage { get; set; }
        public int CardScore { get; set; }
        public CardColour Colour { get; set; }
        public static List<CardType> SpecialCards => new List<CardType> 
        {
            CardType.Reset,
            CardType.LoseTwo,
            CardType.SwapDeck,
            CardType.SeeThrough
        };
        public CardType TypeOfCard { get; set; }

        // add an override toString method
    }
}