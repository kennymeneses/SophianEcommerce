using Ecommerce.Models.Entities;
using Ecommerce.Models.InputsBody;
using Ecommerce.Models.Outputs;
using Ecommerce.Models.ResponseStatus;
using System.Threading.Tasks;

namespace Ecommerce.BussinessLayer.LoginManagement
{
    public interface ILoginManager
    {
        OutputToken BuildToken(LoginInput input , User user);
        Task<RequestToken> ValidateLogin(LoginInput input);
    }
}
