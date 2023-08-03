using UnityEngine;
using UnoDos.Cards.Enums;
using UnoDos.Cards.Interfaces;

namespace Assets.Scripts.Sprites
{
    public class RenderSprites
    {
        public Sprite[] __GreenCardSprites;
        public Sprite[] __MinusTwoCardSprites;
        public Sprite[] __OrangeCardSprites;
        public Sprite[] __PinkCardSprites;
        public Sprite[] __PurpleCardSprites;
        public Sprite[] __ResetCardSprites;
        public Sprite[] __SeeThroughSprite;
        public Sprite[] __SwapDeckCardSprites;

        public RenderSprites(Sprite[] greenCardSprites, Sprite[] orangeCardSprites, Sprite[] pinkCardSprites, Sprite[] purpleCardSprites, Sprite[] seeThroughSprite, Sprite[] resetCardSprites, Sprite[] swapDeckCardSprites, Sprite[] minusTwoCardSprites)
        {
            __GreenCardSprites = greenCardSprites;
            __OrangeCardSprites = orangeCardSprites;
            __PinkCardSprites = pinkCardSprites;
            __PurpleCardSprites = purpleCardSprites;
            __SeeThroughSprite = seeThroughSprite;
            __ResetCardSprites = resetCardSprites;
            __SwapDeckCardSprites = swapDeckCardSprites;
            __MinusTwoCardSprites = minusTwoCardSprites;
        }

        public Sprite GetSprite(ICard cardDrawn)
        {

            Sprite[] _SpriteSetToUse = __SeeThroughSprite;
            CardColour _CardColour = cardDrawn.Colour;
            if (cardDrawn.TypeOfCard != CardType.SeeThrough)
            {
                switch (_CardColour)
                {
                    case CardColour.Orange:
                        if (cardDrawn.TypeOfCard == CardType.Reset)
                        {
                            _SpriteSetToUse = __ResetCardSprites;
                            return _SpriteSetToUse[0];
                        }
                        else if (cardDrawn.TypeOfCard == CardType.LoseTwo)
                        {
                            _SpriteSetToUse = __MinusTwoCardSprites;
                            return _SpriteSetToUse[0];
                        }
                        else if (cardDrawn.TypeOfCard == CardType.SwapDeck)
                        {
                            _SpriteSetToUse = __SwapDeckCardSprites;
                            return _SpriteSetToUse[0];
                        }
                        else
                        {
                            _SpriteSetToUse = __OrangeCardSprites;
                            return _SpriteSetToUse[cardDrawn.CardScore];
                        }

                    case CardColour.Pink:
                        if (cardDrawn.TypeOfCard == CardType.Reset)
                        {
                            _SpriteSetToUse = __ResetCardSprites;
                            return _SpriteSetToUse[1];
                        }
                        else if (cardDrawn.TypeOfCard == CardType.LoseTwo)
                        {
                            _SpriteSetToUse = __MinusTwoCardSprites;
                            return _SpriteSetToUse[1];
                        }
                        else if (cardDrawn.TypeOfCard == CardType.SwapDeck)
                        {
                            _SpriteSetToUse = __SwapDeckCardSprites;
                            return _SpriteSetToUse[1];
                        }
                        else
                        {
                            _SpriteSetToUse = __PinkCardSprites;
                            return _SpriteSetToUse[cardDrawn.CardScore];
                        }

                    case CardColour.Green:
                        if (cardDrawn.TypeOfCard == CardType.Reset)
                        {
                            _SpriteSetToUse = __ResetCardSprites;
                            return _SpriteSetToUse[2];
                        }
                        else if (cardDrawn.TypeOfCard == CardType.LoseTwo)
                        {
                            _SpriteSetToUse = __MinusTwoCardSprites;
                            return _SpriteSetToUse[2];
                        }
                        else if (cardDrawn.TypeOfCard == CardType.SwapDeck)
                        {
                            _SpriteSetToUse = __SwapDeckCardSprites;
                            return _SpriteSetToUse[2];
                        }
                        else
                        {
                            _SpriteSetToUse = __GreenCardSprites;
                            return _SpriteSetToUse[cardDrawn.CardScore];
                        }

                    case CardColour.Purple:
                        if (cardDrawn.TypeOfCard == CardType.Reset)
                        {
                            _SpriteSetToUse = __ResetCardSprites;
                            return _SpriteSetToUse[3];
                        }
                        else if (cardDrawn.TypeOfCard == CardType.LoseTwo)
                        {
                            _SpriteSetToUse = __MinusTwoCardSprites;
                            return _SpriteSetToUse[3];
                        }
                        else if (cardDrawn.TypeOfCard == CardType.SwapDeck)
                        {
                            _SpriteSetToUse = __SwapDeckCardSprites;
                            return _SpriteSetToUse[3];
                        }
                        else
                        {
                            _SpriteSetToUse = __PurpleCardSprites;
                            return _SpriteSetToUse[cardDrawn.CardScore];
                        }

                }
            }
            return _SpriteSetToUse[0];
        }
    }
}
