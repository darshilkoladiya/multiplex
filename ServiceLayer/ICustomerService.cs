using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomer();
        Customer GetCustomerById(int id);
        Customer Login(Login login);
        void InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}
