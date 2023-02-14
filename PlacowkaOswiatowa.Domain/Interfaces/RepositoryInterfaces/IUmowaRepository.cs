using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IUmowaRepository : IBaseEntityRepository<Umowa, int>
    {
        Task<bool> Exists(UmowaDto umowa);
        bool DoesEmployeeHasAgreement(int employeeId);
        Task<List<Umowa>> GetAllAsync();
        List<Umowa> GetAll();
        Task<Umowa> GetByIdAsync(int id);
    }
}
