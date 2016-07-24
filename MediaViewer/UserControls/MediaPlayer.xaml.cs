using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MediaViewer.Model;

namespace MediaViewer.UserControls
{
    /// <summary>
    /// Interaction logic for MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayer : UserControl
    {
        public static readonly DependencyProperty MediaProperty =
            DependencyProperty.Register(
            "Media",
            typeof(Media),
            typeof(MediaPlayer));

        private bool _userMovingSlider;

        public MediaPlayer()
        {
            InitializeComponent();
        }

        public Media Media
        {
            get { return GetValue(MediaProperty) as Media; }
            set 
            {
                SetValue(MediaProperty, value);
                mediaElement.Clock = null;
                progressSlider.Value = 0;
            }
        }

        private void progressSlider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _userMovingSlider = true;
        }

        private void progressSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MediaClock clock = mediaElement.Clock;
            if(clock != null)
            {
                TimeSpan offset = TimeSpan.FromMilliseconds(progressSlider.Value);
                clock.Controller.Seek(offset, TimeSeekOrigin.BeginTime);
            }
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            progressSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Clock = null;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Clock = null;
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            MediaClock clock = mediaElement.Clock;
            if(clock != null)
            {
                if (clock.IsPaused)
                    clock.Controller.Resume();
                else
                    clock.Controller.Pause();
            }
            else
            {
                if (Media == null) return;
                MediaTimeline timeline = new MediaTimeline(Media.Uri);
                clock = timeline.CreateClock();
                clock.CurrentTimeInvalidated += Clock_CurrentTimeInvalidated;
                mediaElement.Clock = clock;
            }
        }

        private void Clock_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            if (mediaElement.Clock == null || _userMovingSlider)
                return;

            progressSlider.Value = mediaElement.Clock.CurrentTime.Value.TotalMilliseconds;
        }
    }
}
