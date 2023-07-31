using UnoDos.Cards.Enums;

namespace UnoDos.Cards.Interfaces
{
    public interface ICard
    {
        ICard CreateCard(int cardID, CardColour colour, CardType cardType, int cardScore);
        string ToString();

        int CardID { get; set; }
        int CardScore { get; set; }
        CardColour Colour { get; set; }
        CardType TypeOfCard { get; set; }
    }
}