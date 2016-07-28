using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicineBoxRecorder.Model
{
    public enum BOX_TYPE {ALL, UNTOUCHED, TOUCHED}
    /// <summary>
    /// 实现数据访问,保存,导出至数据库
    /// </summary>
    public interface IDataAccess
    {
        void InitBox(int startIdx, int endIdx);
        List<Box> GetBoxList(BOX_TYPE type);
        void SaveBoxBinding(string macID, string cardID);
        bool SaveToExcel();
    }
}
