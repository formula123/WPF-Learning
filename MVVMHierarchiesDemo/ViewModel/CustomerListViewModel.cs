using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMHierarchiesDemo.Model;

namespace MVVMHierarchiesDemo.ViewModel
{
    class CustomerListViewModel : BindableBase
    {
        public Customer Cust { get; set; }

        public CustomerListViewModel()
        {
            Cust = new Customer { Name = "rubyHuang" };
        }
    }
}
