using System.ComponentModel;

namespace PlacowkaOswiatowa.Domain.Enums
{
    public enum EtatEnum
    {
        [Description("Pracownik Administracyjny")]
        PracownikAdministracyjny,
        [Description("Pracownik Pedagogiczny")]
        PracownikPedagogiczny,
        [Description("Pracownik Obsługi")]
        PracownikObslugi
    }
}
