using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace MediaViewer.Model
{
    public class Media : INotifyPropertyChanged
    {
        protected FileInfo _fileInfo;
        protected Uri _uri;

        public string Name
        {
            get { return Path.GetFileNameWithoutExtension(_fileInfo.Name); }
        }

        public string Directory
        {
            get { return _fileInfo.Directory.Name; }
        }

        public Uri Uri
        {
            get { return _uri; }
        }

        public void SetFile(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
            _uri = new Uri(_fileInfo.FullName);


        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
