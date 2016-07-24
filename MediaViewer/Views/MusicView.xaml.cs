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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaViewer.UserControls;

namespace MediaViewer.Views
{
    /// <summary>
    /// Interaction logic for MusicView.xaml
    /// </summary>
    public partial class MusicView : UserControl
    {
        public MusicView()
        {
            InitializeComponent();
        }

        private void songList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bindingExpression = mPlayer.GetBindingExpression(MediaPlayer.MediaProperty);
            bindingExpression.UpdateTarget();
        }
    }
}
