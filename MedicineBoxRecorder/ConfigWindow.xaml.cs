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
using System.Windows.Shapes;
using MedicineBoxRecorder.Helper;
using MedicineBoxRecorder.Model;
using System.IO.Ports;

namespace MedicineBoxRecorder
{
    /// <summary>
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWindow : Window
    {
        private MainWindow _parent;
        private PublicClass _configInfo;
        public ConfigWindow(MainWindow parent, PublicClass configInfo)
        {
            InitializeComponent();

            _parent = parent;
            _configInfo = configInfo;
            this.DataContext = configInfo;

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                boxerCbx.Items.Add(port);
                readerCbx.Items.Add(port);
            }

            Closing +=ConfigWindow_Closing;
        }

        private void ConfigWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("是否已保存所有配置？", "系统提示", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
            {
                this.Hide();
            }
        }

        private void BoxSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rt = MessageBox.Show("保存后会重新分配药框编号，是否要继续保存？","提示信息", MessageBoxButton.OKCancel);
            if (rt == MessageBoxResult.OK)
            {
                try
                {
                    _configInfo.SaveConfig();
                    _parent.InitBox(int.Parse(startTbx.Text.Trim()), int.Parse(endTbx.Text.Trim()));
                }
                catch (System.Exception)
                {
                    MessageBox.Show("请正确输入参数");
                }

            }
        }

        private void devSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rt = MessageBox.Show("是否需要保存？", "提示信息", MessageBoxButton.OKCancel);
            if (rt == MessageBoxResult.OK)
            {
                try
                {
                    _configInfo.SaveConfig();
                }
                catch (System.Exception)
                {
                    MessageBox.Show("请正确输入参数");
                }
            }
        }
    }
}
