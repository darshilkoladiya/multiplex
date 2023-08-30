using Data.Context;
using Data.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class CustomerService : ICustomerService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMultiRepository<Customer> customerRepository;

        public CustomerService(IMultiRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetCustomer()
        {
            return customerRepository.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return customerRepository.Get(id);
        }

        public void InsertCustomer(Customer customer)
        {
            customerRepository.Insert(customer);
        }
        public void UpdateCustomer(Customer customer)
        {
            customerRepository.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            
            Customer customer = GetCustomerById(id);
            customer.IsDeleted = true;
            UpdateCustomer(customer);
            //customerRepository.Remove(customer);
            //customerRepository.SaveChanges();
        }

        public Customer Login(Login login)
        {
            var customer = db.customers.Where(a => a.Email.Equals(login.Email) && a.Password.Equals(login.Password)).FirstOrDefault();
            return customer;
        }

       
    }
}
