using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnoDos.Cards.Enums;
using UnoDos.Cards.Interfaces;

namespace Assets.Scripts.Sprites
{
    public class RenderSprites
    {

        public Sprite[] GreenCardSprites;
        public Sprite[] OrangeCardSprites;
        public Sprite[] PinkCardSprites;
        public Sprite[] PurpleCardSprites;
        public Sprite[] SeeThroughSprite;
        public Sprite[] ResetCardSprites;
        public Sprite[] SwapDeckCardSprites;
        public Sprite[] MinusTwoCardSprites;

        public RenderSprites(Sprite[] greenCardSprites, Sprite[] orangeCardSprites, Sprite[] pinkCardSprites, Sprite[] purpleCardSprites, Sprite[] seeThroughSprite, Sprite[] resetCardSprites, Sprite[] swapDeckCardSprites, Sprite[] minusTwoCardSprites)
        {
            GreenCardSprites = greenCardSprites;
            OrangeCardSprites = orangeCardSprites;
            PinkCardSprites = pinkCardSprites;
            PurpleCardSprites = purpleCardSprites;
            SeeThroughSprite = seeThroughSprite;
            ResetCardSprites = resetCardSprites;
            SwapDeckCardSprites = swapDeckCardSprites;
            MinusTwoCardSprites = minusTwoCardSprites;
        }

        public Sprite GetSprite(ICard cardDrawn)
        {

            Sprite[] _SpriteSetToUse = SeeThroughSprite;
            CardColour _CardColour = cardDrawn.Colour;

            switch (_CardColour)
            {
                case CardColour.Orange:
                    if (cardDrawn.TypeOfCard == CardType.Reset)
                    {
                        _SpriteSetToUse = ResetCardSprites;
                        return _SpriteSetToUse[0];
                    }
                    else if (cardDrawn.TypeOfCard == CardType.LoseTwo)
                    {
                        _SpriteSetToUse = MinusTwoCardSprites;
                        return _SpriteSetToUse[0];
                    }
                    else if (cardDrawn.TypeOfCard == CardType.SwapDeck)
                    {
                        _SpriteSetToUse = SwapDeckCardSprites;
                        return _SpriteSetToUse[0];
                    }
                    else
                    {
                        _SpriteSetToUse = OrangeCardSprites;
                        return _SpriteSetToUse[cardDrawn.CardScore];
                    }
                        
                case CardColour.Pink:
                    if (cardDrawn.TypeOfCard == CardType.Reset)
                    {
                        _SpriteSetToUse = ResetCardSprites;
                        return _SpriteSetToUse[1];
                    }
                    else if (cardDrawn.TypeOfCard == CardType.LoseTwo)
                    {
                        _SpriteSetToUse = MinusTwoCardSprites;
                        return _SpriteSetToUse[1];
                    }
                    else if (cardDrawn.TypeOfCard == CardType.SwapDeck)
                    {
                        _SpriteSetToUse = SwapDeckCardSprites;
                        return _SpriteSetToUse[1];
                    }
                    else
                    {
                        _SpriteSetToUse = PinkCardSprites;
                        return _SpriteSetToUse[cardDrawn.CardScore];
                    }

                case CardColour.Green:
                    if (cardDrawn.TypeOfCard == CardType.Reset)
                    {
                        _SpriteSetToUse = ResetCardSprites;
                        return _SpriteSetToUse[2];
                    }
                    else if (cardDrawn.TypeOfCard == CardType.LoseTwo)
                    {
                        _SpriteSetToUse = MinusTwoCardSprites;
                        return _SpriteSetToUse[2];
                    }
                    else if (cardDrawn.TypeOfCard == CardType.SwapDeck)
                    {
                        _SpriteSetToUse = SwapDeckCardSprites;
                        return _SpriteSetToUse[2];
                    }
                    else
                    {
                        _SpriteSetToUse = GreenCardSprites;
                        return _SpriteSetToUse[cardDrawn.CardScore];
                    }

                case CardColour.Purple:
                    if (cardDrawn.TypeOfCard == CardType.Reset)
                    {
                        _SpriteSetToUse = ResetCardSprites;
                        return _SpriteSetToUse[3];
                    }
                    else if (cardDrawn.TypeOfCard == CardType.LoseTwo)
                    {
                        _SpriteSetToUse = MinusTwoCardSprites;
                        return _SpriteSetToUse[3];
                    }
                    else if (cardDrawn.TypeOfCard == CardType.SwapDeck)
                    {
                        _SpriteSetToUse = SwapDeckCardSprites;
                        return _SpriteSetToUse[3];
                    }
                    else
                    {
                        _SpriteSetToUse = PurpleCardSprites;
                        return _SpriteSetToUse[cardDrawn.CardScore];
                    }
                   
            }
        return _SpriteSetToUse[0];
        }
    }
}
