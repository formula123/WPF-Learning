using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BindingSource
{
    public class Caculator
    {
        public string add(string arg1, string arg2)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            if (double.TryParse(arg1, out x) && double.TryParse(arg2, out y))
            {
                z = x + y;
                return z.ToString();
            }
            return "input error";
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetBindings();
        }

        private void SetBindings()
        {
            ObjectDataProvider odp = new ObjectDataProvider();
            odp.ObjectInstance = new Caculator();
            odp.MethodName = "add";
            odp.MethodParameters.Add("0");
            odp.MethodParameters.Add("0");

            Binding bd1 = new Binding("MethodParameters[0]") {
                Source=odp,
                BindsDirectlyToSource=true,
                UpdateSourceTrigger=UpdateSourceTrigger.PropertyChanged
            };

            Binding bd2 = new Binding("MethodParameters[1]") {
                Source=odp,
                BindsDirectlyToSource=true,
                UpdateSourceTrigger=UpdateSourceTrigger.PropertyChanged
            };

            Binding bd3 = new Binding(".")
            {
                Source = odp
            };

            tbx1.SetBinding(TextBox.TextProperty, bd1);
            tbx2.SetBinding(TextBox.TextProperty, bd2);

            sum.SetBinding(TextBlock.TextProperty, bd3);
        }
    }
}
