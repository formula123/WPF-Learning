﻿using System;
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

namespace ContactManager.UserControls
{
    /// <summary>
    /// SideBar.xaml 的交互逻辑
    /// </summary>
    public partial class SideBar : UserControl
    {
        public SideBar()
        {
            InitializeComponent();
        }
        public ApplicationPresenter Presenter
        {
            get { return DataContext as ApplicationPresenter; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Presenter.NewContact();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Presenter.DisplayAllContacts();
        }

        private void OpenContact_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;
            if (button != null)
                Presenter.OpenContact(button.DataContext as Contact);
        }
    }
}
