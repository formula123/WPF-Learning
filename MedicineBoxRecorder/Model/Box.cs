using System;
using FileHelpers;

namespace MedicineBoxRecorder.Model
{
    [Serializable]
    [DelimitedRecord(",")]
    public class Box : Notifier
    {
        private string _machineID;
        public string MachineID
        {
            get { return _machineID; }
            set
            {
                _machineID = value;
                OnPropertyChanged("MachineID");
            }
        }

        private string _cardID;
        public string CardID
        {
            get { return _cardID; }
            set
            {
                _cardID = value;
                OnPropertyChanged("CardID");
            }
        }
    }
}
