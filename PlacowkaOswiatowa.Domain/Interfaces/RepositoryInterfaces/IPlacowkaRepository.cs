using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IPlacowkaRepository
    {
        IAdresRepository Adresy { get; }
        IPracownikRepository Pracownicy { get; }
        IUczenRepository Uczniowie { get; }
        IOddzialRepository Oddzialy { get; }
        IUrlopRepository Urlopy { get; }
        IEtatRepository Etaty { get; }
        IStanowiskoRepository Stanowiska { get; }
        IPrzedmiotRepository Przedmioty { get; }
        IOcenaRepository Oceny { get; }
        bool CanConnect();
        Task SaveAsync();
    }
}
