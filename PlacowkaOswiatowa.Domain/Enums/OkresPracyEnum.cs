using System.ComponentModel;

namespace PlacowkaOswiatowa.Domain.Enums
{
    public enum OkresPracyEnum
    {
        [Description("czas określony")]
        Okreslony,
        [Description("czas nieokreślony")]
        Nieokreslony,
        [Description("okres próbny")]
        Probny
    }
}
