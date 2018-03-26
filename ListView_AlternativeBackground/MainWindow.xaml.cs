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

namespace ListView_AlternativeBackground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonVictor_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DependencyObject lvl1 = VisualTreeHelper.GetParent(btn);
            DependencyObject lvl2 = VisualTreeHelper.GetParent(lvl1);
            DependencyObject lvl3 = VisualTreeHelper.GetParent(lvl2);
            MessageBox.Show("lvl1:" + lvl1.GetType().ToString() + "\n lvl2:" + lvl2.GetType().ToString() + "\n lvl3:" + lvl3.GetType().ToString());
        }
    }
}
