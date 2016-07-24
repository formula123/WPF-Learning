using System.Collections.ObjectModel;
using System.IO;
using MediaViewer.Model;

namespace MediaViewer.Presenters
{
    public class MediaPresenter<T>
        where T : Media, new()
    {
        private readonly string[] _fileExtensions;
        private readonly string _mediaPath;
        private ObservableCollection<Media> _media;

        public MediaPresenter(string mediaPath, params string[] extensions)
        {
            _mediaPath = mediaPath;
            _fileExtensions = extensions;
        }

        public ObservableCollection<Media> Media
        {
            get
            {
                if (_media == null) LoadMedia();
                return _media;
            }
        }

        private void LoadMedia()
        {
            if (string.IsNullOrEmpty(_mediaPath)) return;

            _media = new ObservableCollection<Media>();

            DirectoryInfo directoryInfo = new DirectoryInfo(_mediaPath);

            foreach(string extension in _fileExtensions)
            {
                FileInfo[] pictureFiles = directoryInfo.GetFiles(
                    extension,
                    SearchOption.AllDirectories);

                foreach(FileInfo fileInfo in pictureFiles)
                {
                    if (_media.Count == 50)
                        break;

                    T media = new T();
                    media.SetFile(fileInfo);
                    _media.Add(media);
                }
            }
        }
    }
}
