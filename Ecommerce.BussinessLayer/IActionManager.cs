using Ecommerce.Models.InputsBody;
using Ecommerce.Models.ResponseStatus;
using System.Threading.Tasks;

namespace Ecommerce.BussinessLayer
{
    public interface IActionManager
    {
        Task<DataQuery> Search(int page, int size);
        Task<SingleQuery> SearchById(int id);
        Task<CheckStatus> Create(BaseInputEntity entity);
        Task<CheckStatus> Update(int id, BaseInputEntity entity);
        Task<CheckStatus> Remove(int id);
        void SaveChanges();
    }
}
