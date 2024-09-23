using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1080053.DomainModels
{
    /// <summary>
    /// thông tin của tài khoản trong SQL (của employee hoặc customer)
    /// </summary>
    public class UserAccount
    {
        public string UserId { get; set; } = "";
        public string UserName { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Photo { get; set; } = "";
    }
}
