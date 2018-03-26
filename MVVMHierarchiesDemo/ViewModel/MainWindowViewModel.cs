using MVVMHierarchiesDemo.ViewModel;
using MVVMHierarchiesDemo.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMHierarchiesDemo.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
        }


        private CustomerListViewModel custListViewModel = new CustomerListViewModel();
        private OrderViewModel orderViewModel = new OrderViewModel();
        private BindableBase _currentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public MyICommand<string> NavCommand { get; private set; }

        private void OnNav(string dest)
        {
            switch (dest)
            {
                case "orders":
                    CurrentViewModel = orderViewModel;
                    break;
                case "customers":
                    CurrentViewModel = custListViewModel;
                    break;
            }
        }
    }
}
