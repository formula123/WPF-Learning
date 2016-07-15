using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContactManager.Presenters;
using ContactManager.Model;

namespace ContactManager.Views
{
    /// <summary>
    /// ContactListView.xaml 的交互逻辑
    /// </summary>
    public partial class ContactListView : UserControl
    {
        public ContactListView()
        {
            InitializeComponent();
        }

        public ContactListPresenter Presenter
        {
            get { return DataContext as ContactListPresenter; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Presenter.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            Presenter.OpenContact(btn.DataContext as Contact);
        }
    }
}
