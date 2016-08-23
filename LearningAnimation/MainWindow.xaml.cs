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
using System.Windows.Media.Animation;

namespace LearningAnimation
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

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            var animation = new ColorAnimation(Colors.Red,Colors.Black, TimeSpan.FromSeconds(5));
            DoubleAnimation widthAnimation =
                new DoubleAnimation(120, 300, TimeSpan.FromSeconds(5));
            widthAnimation.RepeatBehavior = RepeatBehavior.Forever;
            widthAnimation.AutoReverse = true;
            var clock = widthAnimation.CreateClock();
            var button = new Button();
            btnPanel.Children.Add(button);
            //button.BeginAnimation(Button.WidthProperty, widthAnimation);
            //button.BeginAnimation(Button.BackgroundProperty, animation);
            button.ApplyAnimationClock(Button.WidthProperty, clock);
        }
    }
}
