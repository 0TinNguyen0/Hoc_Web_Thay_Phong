using SV20T1080053.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1080053.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên quan đến tài khoản
    /// </summary>
    public interface IUserAccountDAL
    {
        UserAccount? Authorize(string userName, string password);

        bool ChangePassword(string userName, string password);
    }
}
