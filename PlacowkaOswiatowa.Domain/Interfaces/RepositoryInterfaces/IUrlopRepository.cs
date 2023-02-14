using PlacowkaOswiatowa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IUrlopRepository
    {
        Task<List<Urlop>> GetAllAsync();
        List<Urlop> GetAll();
        bool Exists(int idPracownika, DateTime poczatekUrlopu, DateTime koniecUrlopu);
        Task AddAsync(Urlop urlop);
    }
}
