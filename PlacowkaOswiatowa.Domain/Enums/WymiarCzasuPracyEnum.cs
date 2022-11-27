using System.ComponentModel;

namespace PlacowkaOswiatowa.Domain.Enums
{
    public enum WymiarCzasuPracyEnum
    {
        [Description("Pełen etat")]
        Pelen,
        [Description("Pół etatu")]
        Polowa,
        [Description("Ćwierć etatu")]
        Cwierc,
        [Description("Inny")]
        Inny
    }
}
