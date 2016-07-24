using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaViewer.Views;
using System.Windows.Controls;
using MediaViewer.Model;

namespace MediaViewer.Presenters
{
    public class MenuPresenter
    {
        private readonly ApplicationController _controller;

        public MenuPresenter(ApplicationController controller)
        {
            _controller = controller;
            _controller.DisplayInShell(new MenuView(this));
        }

        public void DisplayPictures()
        {
            //get public picture folder in computer
            string myPicturesPath = Environment.GetFolderPath(
                Environment.SpecialFolder.MyPictures);

            Display<PictureView, Picture>(
                myPicturesPath,
                "*.jpg", "*.gif", "*.png", "*.bmp");
        }

        public void ListenToMusic()
        {

        }

        public void WatchVideo()
        {

        }

        private void Display<View, MediaType>(
            string mediaPath,
            params string[] extensions)
            where View : UserControl, new()
            where MediaType : Media, new()
        {
            MediaPresenter<MediaType> presenter =
                new MediaPresenter<MediaType>(mediaPath, extensions);

            View view = new View();

            view.DataContext = presenter;

            _controller.DisplayInShell(view);
        }
    }
}
