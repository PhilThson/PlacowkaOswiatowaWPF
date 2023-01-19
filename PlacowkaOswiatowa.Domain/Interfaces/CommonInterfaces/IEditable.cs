using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces
{
    public interface IEditable
    {
        Task LoadItem(object objId);
    }
}
