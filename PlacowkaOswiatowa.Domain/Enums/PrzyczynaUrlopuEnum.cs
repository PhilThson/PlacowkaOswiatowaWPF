using System.ComponentModel;

namespace PlacowkaOswiatowa.Domain.Enums
{
    public enum PrzyczynaUrlopuEnum
    {
        Wypoczynkowy,
        Zdrowotny,
        [Description("Macierzyński")]
        Macierzynski,
        [Description("Tacierzyński")]
        Tacierzynski,
        Wychowawczy,
        [Description("Okolicznościowy")]
        Okolicznosciowy,
        [Description("Długotrwały")]
        Dlugotrwaly,
        [Description("Opieka nad dzieckiem")]
        OpiekaNadDzieckiem
    }
}
