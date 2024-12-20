using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Genie_Model;
using ERP_Genie_Model.DTO;
using ERP_Genie_Model.Model;

namespace ERP_Genie_Service.Contract.Interfaces
{
    public interface IAuthService
    {
        Task<BaseReponse<string>> Login(LoginModel login);
        Task<BaseReponse<string>> Logout();
        Task<BaseReponse<string>> Register(SignupModel signup);
    }
}
