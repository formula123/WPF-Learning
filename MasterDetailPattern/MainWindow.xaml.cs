using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterDetailPattern
{
    public class Team: INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    public class Division: INotifyPropertyChanged
    {
        private string _name;
        private ObservableCollection<Team> _teams;

        public string Name
        {
            get { return _name; }
            set { _name = value;OnPropertyChanged(nameof(Name)); }
        }
        public ObservableCollection<Team> Teams
        {
            get { return _teams; }
            set { _teams = value; OnPropertyChanged(nameof(Teams)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    public class League: INotifyPropertyChanged
    {
        private ObservableCollection<Division> _division;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public ObservableCollection<Division> Divisions
        {
            get { return _division; }
            set { _division = value; OnPropertyChanged(nameof(Divisions)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        public ObservableCollection<League> Leagues { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var teams = new ObservableCollection<Team>
            {
                new Team { Name = "zzl" },
                new Team { Name = "zzl1" },
                new Team { Name = "zzl2" },
                new Team { Name = "zzl3" },
                new Team { Name = "zzl4" }
            };

            var devisons = new ObservableCollection<Division>
            {
                new Division
                {
                    Name="A",
                    Teams=teams
                },
                new Division
                {
                    Name="B",
                    Teams=teams
                },
                new Division
                {
                    Name="C",
                    Teams=teams
                },
                new Division
                {
                    Name="D",
                    Teams=teams
                },
            };

            Leagues = new ObservableCollection<League>
            {
                new League
                {
                    Name = "League A",
                    Divisions = devisons
                },
                new League
                {
                    Name = "League B",
                    Divisions = devisons
                }
            };

            OnPropertyChanged(nameof(Leagues));
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
