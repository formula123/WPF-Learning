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

namespace Controls.ListBox
{
    /// <summary>
    /// Interaction logic for ListBoxItems.xaml
    /// </summary>
    public partial class ListBoxItems : UserControl
    {
        public ListBoxItems()
        {
            InitializeComponent();

            Items = new List<TodoItem>();
            for (int i = 0; i < 9; i++)
            {
                Items.Add(new TodoItem { Title = "ListBoxItem" + i.ToString() });
            }

            this.DataContext = this;
        }

        public List<TodoItem> Items { get; set; }
        public class TodoItem
        {
            public string Title { get; set; }
        }


        //使用该函数是为了解决当我点击时listbox不能实时更新选项
        private void ListBoxItem_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //ListBoxItem item = (ListBoxItem)sender;
            //item.IsSelected = true;
            //msgTbx.Text = msgTbx.Text + "Keyboard";
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            item.IsSelected = true;
            //msgTbx.Text = msgTbx.Text + "Mouse";
        }
    }
}
