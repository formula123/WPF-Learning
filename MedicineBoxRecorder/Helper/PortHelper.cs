using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MedicineBoxRecorder.Helper
{
    public enum CMDType
    {
        MedIn,
        LEDFresh,
        LEDOff,
        ShutDownAll,
        ShutDownSelected,
        LEDFreshTwo,
        LEDFreshWait,
    }

    public class PortHelper
    {
        private const byte ShutDownAll = 0xF1;
        private const byte ShutDownSelected = 0xF8;
        private const byte Medin = 0xF2;
        private const byte Ledoff = 0xF3;
        private const byte Ledfresh = 0xF6;
        private const byte LedfreshTwo = 0xF9;
        private const byte LedfreshWait = 0xFA;

        /// <summary>
        /// 设定亮灯颜色
        /// </summary>
        /// <param name="num">数字</param>
        /// <returns>成功为true，否则false</returns>
        public static bool SetColor(int num)
        {
            int res = Set_Ligt(2, num);
            return res == 1 ? true : false;
        }

        /// <summary>
        /// 向串口发送命令，亮灯命令
        /// </summary>
        /// <param name="ID">药框编号</param>
        /// <param name="sendCMD">命令类型</param>
        /// <param name="sendMark">发送时间</param>
        /// <param name="strErr">返回错误信息</param>
        /// <returns>成功发送为true，否则为false</returns>
        public static bool SendCMD(int ID, CMDType sendCMD, int sendMark, ref string strErr)
        {
            try
            {
                byte HAddr = 0x0;
                byte LAddr = 0x0;
                byte CMD = 0x0;
                byte Mark = 0x0;
                int CMDRe = 0;
                HAddr = (byte)(ID / 256);
                LAddr = (byte)(ID % 256);
                switch (sendCMD)
                {
                    case CMDType.LEDFresh:
                        CMD = Ledfresh;
                        Mark = (byte)sendMark;
                        break;
                    case CMDType.LEDOff:
                        CMD = Ledoff;
                        break;
                    case CMDType.MedIn:
                        CMD = Medin;
                        break;
                    case CMDType.ShutDownAll:
                        CMD = ShutDownAll;
                        break;
                    case CMDType.ShutDownSelected:
                        CMD = ShutDownSelected;
                        break;
                    case CMDType.LEDFreshTwo:
                        CMD = LedfreshTwo;
                        break;
                    case CMDType.LEDFreshWait:
                        CMD = LedfreshWait;
                        break;
                }

                CMDRe = Send_Message(HAddr, LAddr, CMD, Mark);
                strErr = ErrExp(CMDRe);
                return (CMDRe / 0xFFFFFF).ToString().PadLeft(2, '0') == "01" ? true : false;

            }
            catch (System.Exception ex)
            {
                strErr = ex.Message;
                return false;
            }

        }
        private static string ErrExp(int errCode)
        {
            string strErr= string.Empty;
            int addr = 0;
            string flag = string.Empty;

            strErr = (errCode % 256).ToString("X2");
            addr = (errCode % 0xFFFFFF) / 256;

            string rt = addr.ToString() + "号框 ";

            switch (strErr)
            {
                case "F1":
                    rt += "关机命令发送";
                    break;
                case "F2":
                    rt += "装药命令发送";
                    break;
                case "F3":
                    rt += "红灯闪停命令发送";
                    break;
                case "F4":
                    rt += "电量低";
                    break;
                case "F6":
                    rt += "设置红灯闪烁时间命令发送";
                    break;
                case "F7":
                    rt += "处于非工作状态";
                    break;
                case "F8":
                    rt += "单个药框关机命令发送";
                    break;
                case "F9":
                    rt += "发送就绪命令";
                    break;
                case "FA":
                    rt += "发送设定就绪闪烁时间命令";
                    break;
                default:
                    rt = "未知命令";
                    break;
            }
            return rt;
        }

        public static bool OpenPort(string portNum, int baudRate)
        {        
            try
            {
                bool rt = false;
                if (Open_Port(ref portNum))
                {
                    if (Set_Port(baudRate))
                    {
                        rt = true;
                    }
                    else
                    {
                        rt = false;
                    }
                }
                else
                {
                    rt = false;
                }
                return rt;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public static bool ClosePort()
        {
            return Close_Port();
        }

        [DllImport("Port_Test10.DLL", EntryPoint = "_Send_Message@16")]
        private static extern int Send_Message(byte addrHight, byte addrLow, byte orders, byte marks);
         
        [DllImport("Port_Test10.DLL", EntryPoint = "_Change_num@8")]
        private static extern int Set_Ligt(int one, int two);

        [DllImport("Port_Test10.DLL", EntryPoint = "_Open_Port@4", CharSet=CharSet.Unicode)]
        private static extern bool Open_Port(ref string portNum);

        [DllImport("Port_Test10.DLL", EntryPoint = "_Close_Port@0")]
        private static extern bool Close_Port();

        [DllImport("Port_Test10.DLL", EntryPoint = "_Check_Port@0")]
        private static extern int Check_Port();

        [DllImport("Port_Test10.DLL", EntryPoint = "_Set_Port@4")]
        private static extern bool Set_Port(int baudRate);
    }
}
