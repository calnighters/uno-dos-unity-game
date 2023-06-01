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
        public Sprite[] SpecialCardSprites;

        public RenderSprites(Sprite[] greenCardSprites, Sprite[] orangeCardSprites, Sprite[] pinkCardSprites, Sprite[] purpleCardSprites, Sprite[] specialCardSprites)
        {
            GreenCardSprites = greenCardSprites;
            OrangeCardSprites = orangeCardSprites;
            PinkCardSprites = pinkCardSprites;
            PurpleCardSprites = purpleCardSprites;
            SpecialCardSprites = specialCardSprites;
        }

        public Sprite GetSprite(ICard cardDrawn)
        {

            Sprite[] _SpriteSetToUse = SpecialCardSprites;


            if (cardDrawn.CardScore < 9)
            {
                CardColour _CardColour = cardDrawn.Colour;
                switch (_CardColour)
                {
                    case CardColour.Orange:
                        _SpriteSetToUse = OrangeCardSprites;
                        break;
                    case CardColour.Pink:
                        _SpriteSetToUse = PinkCardSprites;
                        break;
                    case CardColour.Green:
                        _SpriteSetToUse = GreenCardSprites;
                        break;
                    case CardColour.Purple:
                        _SpriteSetToUse = PurpleCardSprites;
                        break;
                }

                return _SpriteSetToUse[cardDrawn.CardScore];
            }

            switch (cardDrawn.TypeOfCard)
            {
                case CardType.Reset:
                    return _SpriteSetToUse[0];
                case CardType.LoseTwo:
                    return _SpriteSetToUse[1];
                case CardType.SwapDeck:
                    return _SpriteSetToUse[2];
                default:
                    return _SpriteSetToUse[3];
            }
        }
    }
}
