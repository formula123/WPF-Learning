using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHierarchiesDemo.Model
{
    class Customer : BindableBase
    {
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                SetProperty(ref _name, value);
            }
        }
        private string _name;
    }
}
