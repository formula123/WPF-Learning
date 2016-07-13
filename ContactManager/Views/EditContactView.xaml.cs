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
using Microsoft.Win32;

namespace ContactManager.Views
{
    /// <summary>
    /// EditContactView.xaml 的交互逻辑
    /// </summary>
    public partial class EditContactView : UserControl
    {
        public EditContactView()
        {
            InitializeComponent();
        }

        public EditContactPresenter Presenter
        {
            get { return DataContext as EditContactPresenter; }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Presenter.Save();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Presenter.Delete();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Presenter.Close();
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            Presenter.SelectImage();
        }

        public string AskUserForImagePath()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            return dlg.FileName;
        }
    }
}
