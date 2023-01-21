﻿using PlacowkaOswiatowa.Domain.Models.Base;
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
        IUmowaRepository Umowy { get; }
        IPracodawcaRepository Pracodawcy { get; }
        bool CanConnect();
        Task SaveAsync();

        T GetByName<T>(string names)
            where T : BaseDictionaryEntity<int>;

        public T GetById<T>(int id)
            where T : BaseEntity<int>;

        Task AddEntityAsync<T>(T entity)
            where T : class;

        public void DeleteEntity<T>(T entityToDelete)
            where T : BaseEntity<int>;
    }
}
