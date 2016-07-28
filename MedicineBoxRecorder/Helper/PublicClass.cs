using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicineBoxRecorder.Helper
{
    public class PublicClass:Notifier
    {
        public PublicClass()
        {
            InitConfig();
        }
        //开始编号
        public int _startIdx;
        public int StartIdx
        {
            get { return _startIdx; }
            set
            {
                _startIdx = value;
                OnPropertyChanged("StartIdx");
            }
        }

        //结束编号
        public int _endIdx;
        public int EndIdx
        {
            get { return _endIdx; }
            set
            {
                _endIdx = value;
                OnPropertyChanged("EndIdx");
            }
        }

        //读卡器串口
        public string _readerCOM;
        public string ReaderCOM
        {
            get { return _readerCOM; }
            set
            {
                _readerCOM = value;
                OnPropertyChanged("ReaderCOM");
            }
        }

        //药框串口
        public string _boxerCOM;
        public string BoxerCOM
        {
            get { return _boxerCOM; }
            set
            {
                _boxerCOM = value;
                OnPropertyChanged("BoxerCOM");
            }
        }

        //结束符
        public string _endStr;
        public string EndStr
        {
            get { return _endStr; }
            set
            {
                _endStr = value;
                OnPropertyChanged("EndStr");
            }
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        public void InitConfig()
        {
            StartIdx = Convert.ToInt32(ConfigHelper.GetAppConfig("StartIndex"));
            EndIdx = Convert.ToInt32(ConfigHelper.GetAppConfig("EndIndex"));
            ReaderCOM = ConfigHelper.GetAppConfig("ReaderCOM");
            BoxerCOM = ConfigHelper.GetAppConfig("BoxerCOM");
            EndStr = ConfigHelper.GetAppConfig("EndStr");
        }

        public bool SaveConfig()
        {
            bool rt = ConfigHelper.UpdateAppConfig("StartIndex", StartIdx.ToString());
            rt = ConfigHelper.UpdateAppConfig("EndIndex", EndIdx.ToString());
            rt = ConfigHelper.UpdateAppConfig("ReaderCOM", ReaderCOM);
            rt = ConfigHelper.UpdateAppConfig("BoxerCOM", BoxerCOM);
            rt = ConfigHelper.UpdateAppConfig("EndStr", EndStr);

            return rt;
        }
    }
}
