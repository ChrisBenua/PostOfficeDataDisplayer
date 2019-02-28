using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        private ObservableCollection<PostOffice> _postOffices;

        public ObservableCollection<PostOffice> PostOffices
        {
            get => _postOffices;

            set
            {
                _postOffices = value;
                _postOffices.CollectionChanged += (s, e) => OnPropertyChanged("PostOffices");
                OnPropertyChanged();
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
