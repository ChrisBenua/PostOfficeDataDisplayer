using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using PostOfficesDataDisplayer.Models;
using PostOfficesDataDisplayer.Utils;

namespace PostOfficesDataDisplayer.ViewModels
{
    public class PostOfficeDisplayerViewModel: INotifyPropertyChanged
    {

        public static readonly int[] IntegerColumns = new int[] { 0, 3, 13, 18, 21 };

        public static readonly int[] DoubleColumns = new int[] {19, 20};

        public static readonly int MaxLenForNumberColumns = 50;

        public static readonly int MaxLenForStringColumns = 1000;

        private int _prefixCount;

        public int PrefixCount
        {
            get => _prefixCount;

            set
            {
                _prefixCount = value;
                OnPropertyChanged();
                OnPropertyChanged("PostOfficesPrefix");
            }
        }

        private ObservableCollection<PostOffice> _postOfficesPrefix;

        public ObservableCollection<PostOffice> PostOfficesPrefix
        {
            get => new ObservableCollection<PostOffice>( PostOffices.Take(PrefixCount));

        }

        private ObservableCollection<PostOffice> _postOffices;

        public ObservableCollection<PostOffice> PostOffices
        {
            get => _postOffices;

            set
            {
                _postOffices = value;
                _postOffices.CollectionChanged += (s, e) => OnPropertyChanged("PostOffices");
                OnPropertyChanged();
                OnPropertyChanged("PostOfficesPrefix");
            }
        }

        private PostOffice _selectedOffice;

        public PostOffice SelectedOffice
        {
            get => _selectedOffice;

            set
            {
                _selectedOffice = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new RelayCommand(obj =>
                {
                    int index = PostOffices.IndexOf(SelectedOffice);

                    if (index == -1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    PostOffices.RemoveAt(index);
                    OnPropertyChanged("PostOfficesPrefix");                    
                }));
            }
        }

        private RelayCommand _addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand(obj =>
                {
                    PostOffices.Add(new PostOffice());
                    OnPropertyChanged("PostOfficesPrefix");
                }));
            }
        }

        private RelayCommand _openFileCommand;

        public RelayCommand OpenFileCommand
        {
            get
            {
                return _openFileCommand ?? (_openFileCommand = new RelayCommand(obj => 
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "Excel Files|*.csv;";
                    dialog.DefaultExt = ".csv";

                    if (dialog.ShowDialog() == true)
                    {
                        string filePath = dialog.FileName;
                        this.PostOffices = new ObservableCollection<PostOffice>(IOHelper.ReadData(filePath));
                        OnPropertyChanged("PostOfficesPrefix");
                    }
                }));
            }
        }

        public PostOfficeDisplayerViewModel()
        {
            PostOffices = new ObservableCollection<PostOffice>();
        }

        

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
