using SV20T1080053.DataLayers;
using SV20T1080053.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1080053.BusinessLayers
{
    public static class UserAccountService
    {
        private static readonly IUserAccountDAL employeeUserAccountDB;

        /// <summary>
        /// 
        /// </summary>
        static UserAccountService()
        {
            string connectionString = "server=LAPTOP-9KV8VJ1J;user id=sa;password=123;database=LiteCommerceDB;TrustServerCertificate=true";
            employeeUserAccountDB = new DataLayers.SQLServer.EmployeeUserAccountDAL(connectionString);
        }

        public static object Authorize(string userName, string password, TypeOfAccount employee)
        {
            throw new NotImplementedException();
        }

        public static UserAccount?  userAccount(string userName, string password, TypeOfAccount typeOfAccount)
        {
            switch (typeOfAccount)
            {
                case TypeOfAccount.Employee:
                    return employeeUserAccountDB.Authorize(userName, password);
                default:
                    return null;
            }
        }
    }

    public enum TypeOfAccount {  Employee, UserAccount,
        Customer
    }
}
