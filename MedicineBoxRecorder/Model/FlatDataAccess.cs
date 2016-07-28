using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MedicineBoxRecorder.Helper;
using FileHelpers;

namespace MedicineBoxRecorder.Model
{
    /// <summary>
    /// 使用序列化方式实现IDataAccess
    /// </summary>
    public class FlatDataAccess : IDataAccess
    {
        private readonly string _fileName;
        private List<Box> _boxStore;

        public FlatDataAccess()
        {
            _fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "box.db");
            Deserialize();
        }

        public void InitBox(int startIdx, int endIdx)
        {
            _boxStore.Clear();
            for (int i = startIdx; i <= endIdx; i++ )
            {
                Box box = new Box();
                box.CardID = string.Empty;
                box.MachineID = i.ToString();
                _boxStore.Add(box);
            }
            Serialize();
        }

        public List<Box> GetBoxList(BOX_TYPE type)
        {

            switch (type)
            {
                    //显示所有数据
                case BOX_TYPE.ALL:
                    return new List<Box>(_boxStore);
                    //显示未录入数据
                case BOX_TYPE.UNTOUCHED:
                    {
                        IEnumerable<Box> rt = from c in _boxStore
                                              where c.CardID == string.Empty
                                                  select c;
                        return rt.ToList();
                               
                    }
                    //显示已录入数据
                case BOX_TYPE.TOUCHED:
                    {
                        IEnumerable<Box> rt = from c in _boxStore
                                              where c.CardID != string.Empty
                                              select c;
                        return rt.ToList();
                    }
                default:
                    return new List<Box>();
            }
        }

        public void SaveBoxBinding(string macID, string cardID)
        {
            Box rt = _boxStore.Find(t => t.MachineID == macID);
            rt.CardID = cardID;
            Serialize();
        }

        public bool SaveToExcel()
        {
            try
            {
                var engine = new FileHelperEngine<Box>();
                engine.HeaderText = "药框编号,IC卡编号";
                engine.WriteFile("药框配置表.csv", _boxStore);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private void Serialize()
        {
            using (FileStream stream = File.Open(_fileName, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, _boxStore);
            }
        }

        private void Deserialize()
        {
            if (File.Exists(_fileName))
            {
                using (FileStream stream = File.Open(_fileName, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    _boxStore = (List<Box>)formatter.Deserialize(stream);
                }
            }
            else
            {
                _boxStore = new List<Box>();
            }
        }
    }
}
