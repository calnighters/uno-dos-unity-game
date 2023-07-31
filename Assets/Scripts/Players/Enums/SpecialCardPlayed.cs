using System.ComponentModel;

namespace Assets.Scripts.Players.Enums
{
    public enum SpecialCardPlayed
    {
        [Description("None")]
        None = 0,
        [Description("Lose Two")]
        LoseTwo = 1,
        [Description("Reset")]
        Reset = 2,
        [Description("Swap Deck")]
        SwapDeck = 3
    }
}
