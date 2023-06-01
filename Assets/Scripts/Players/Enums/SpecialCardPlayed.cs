using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
