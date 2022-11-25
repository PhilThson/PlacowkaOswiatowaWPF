using PlacowkaOswiatowa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IUrlopRepository
    {
        Task<IEnumerable<Urlop>> GetAllAsync();
        IEnumerable<Urlop> GetAll();
        bool Exists(int idPracownika, DateTime poczatekUrlopu);
    }
}
