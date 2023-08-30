using Data.Entity;
using Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Factory.ModelPrepare
{
    public interface ICustomerModelFactory
    {
        PagedList<CustomerModel> PrepareCustomerModelForIndex(string searchString, string currentFilter, int? page);
        CustomerModel PrepareCustomerModelForEdit(int id);
        Customer PrepareCustomerModelForEditPost(CustomerModel model);


    }
}