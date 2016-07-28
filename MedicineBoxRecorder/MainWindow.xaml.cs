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
using MedicineBoxRecorder.Model;
using System.Collections.ObjectModel;
using System.IO.Ports;
using MedicineBoxRecorder.Helper;

namespace MedicineBoxRecorder
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Box> _boxStore;
        public ObservableCollection<Box> BoxStore
        {
            get { return _boxStore; }
            set { _boxStore = value; }
        }
        private IDataAccess _dataAccess;
        private ConfigWindow _configWindow;
        private SerialPort _readPort;
        private PublicClass _configInfo;

        public MainWindow()
        {
            InitializeComponent();
            _dataAccess = new FlatDataAccess();
            BoxStore = new ObservableCollection<Box>(_dataAccess.GetBoxList(BOX_TYPE.UNTOUCHED));
            DataContext = this;

            colorCbx.Items.Add("红");
            colorCbx.Items.Add("蓝");

            _configInfo = new PublicClass();
            //打开端口
            InitHardware();

            Closing += OnWindowClosing;
        }

        //添加系统退出事件
        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("真的需要退出系统吗？","系统提示", MessageBoxButton.OKCancel);
            if(res == MessageBoxResult.OK)
            {
                Environment.Exit(0);
            }
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            refreshList();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            _dataAccess.SaveBoxBinding(macTbx.Text.Trim(), cardTbx.Text.Trim());
            refreshList();
        }

        private void lLightBtn_Click(object sender, RoutedEventArgs e)
        {
            string rtStr = string.Empty;
            bool rt = PortHelper.SendCMD(int.Parse(macTbx.Text.Trim()), CMDType.LEDFresh, (int)timeUpDown.Value, ref rtStr);
            if (rt == false)
            {
                MessageBox.Show(rtStr);
            }
        }

        private void colorSetBtn_Click(object sender, RoutedEventArgs e)
        {
            int num = colorCbx.SelectedItem.ToString() == @"蓝" ? 0 : 1;
            if (!PortHelper.SetColor(num))
            {
                MessageBox.Show("设定灯颜色失败！");
            }
        }

        private void configBtn_Click(object sender, RoutedEventArgs e)
        {
            _configWindow = new ConfigWindow(this, _configInfo);
            _configWindow.Show();
            _configWindow.Activate();
            _configWindow.Focus();
        }

        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
           if (_dataAccess.SaveToExcel())
           {
               MessageBox.Show("导出成功");
           }
           else
           {
               MessageBox.Show("导出失败");
           }
        }

        private void loadedCbx_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cbx = e.OriginalSource as CheckBox;
            int type = cbx.IsChecked == true ? 2 : 1;
            if (type == 2)
            {
                infoList.Background = Brushes.LightGreen;
            }
            else
            {
                infoList.Background = Brushes.Red;
            }

            refreshList();
        }

        public void refreshList()
        {
            BoxStore.Clear();
            BOX_TYPE type = loadedCbx.IsChecked == true ? BOX_TYPE.TOUCHED : BOX_TYPE.UNTOUCHED;
            foreach (Box i in _dataAccess.GetBoxList(type))
            {
                BoxStore.Add(i);
            }
        }

        private void InitHardware()
        {
            try
            {
                _readPort = new SerialPort();
                bool rt = SetScan(_readPort, 9600, _configInfo.ReaderCOM);
                if (rt == true)
                {
                    _readPort.DataReceived += new SerialDataReceivedEventHandler(ReaderPort_DataReceived);
                }
                else
                {
                    MessageBoxResult res = MessageBox.Show(_configInfo.ReaderCOM + " 无效，是否打开配置？", "系统提示", MessageBoxButton.YesNo);
                    if(res == MessageBoxResult.Yes)
                    {
                        configBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        return;
                    }
                }

                //发药框端口
                if (!PortHelper.OpenPort(_configInfo.BoxerCOM, 9600))
                {
                    MessageBoxResult res = MessageBox.Show(_configInfo.BoxerCOM + "端口被占用或没有设备,是否打开配置？", "系统提示", MessageBoxButton.YesNo);
                    if(res == MessageBoxResult.Yes)
                    {
                        configBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        return;
                    }
                }
            }
            catch (System.Exception)
            {
                return;
            }

        }

        private void ReaderPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = sender as SerialPort;
            string res = string.Empty;
            if (_configInfo.EndStr == string.Empty)
            {
                res = port.ReadTo("");
            }
            else
            {
                res = port.ReadTo("\r\n");
            }
            
            cardTbx.Text = res;
            port.DiscardInBuffer();
        }

        /// <summary>
        /// 设置和打开串口
        /// </summary>
        /// <param name="currentPort"></param>
        /// <param name="baudRate"></param>
        /// <param name="portName"></param>
        /// <returns></returns>
        private bool SetScan(SerialPort currentPort, int baudRate, string portName)
        {
            try
            {
                currentPort.PortName = portName;
                currentPort.BaudRate = baudRate;
                currentPort.Parity = System.IO.Ports.Parity.None;
                currentPort.DataBits = 8;
                currentPort.StopBits = System.IO.Ports.StopBits.One;
                currentPort.Open();
                currentPort.DiscardInBuffer();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
        
        public void InitBox(int startIdx, int EndIdx)
        {
            _dataAccess.InitBox(startIdx, EndIdx);
            refreshList();
        }
    }
}
