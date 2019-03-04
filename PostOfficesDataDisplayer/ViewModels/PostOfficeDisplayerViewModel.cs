using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using PostOfficesDataDisplayer.Models;
using PostOfficesDataDisplayer.Utils;
using PostOfficesDataDisplayer.Views;

namespace PostOfficesDataDisplayer.ViewModels
{
    public class PostOfficeDisplayerViewModel: INotifyPropertyChanged
    {

        public static readonly int[] IntegerColumns = new int[] { 0, 3, 13, 18, 21 };

        public static readonly int[] DoubleColumns = new int[] {19, 20};

        public static readonly int MaxLenForDoubleColumns = 50;

        public static readonly int MaxLenForIntColumns = 10;

        public static readonly int MaxLenForStringColumns = 1000;

        private int _filterPredicateIndex = 0;

        private int _sortPredicateIndex = 0;

        private string _filterStr;

        public string FilterStr
        {
            get => _filterStr;
            set
            {
                _filterStr = value;
                OnPropertyChanged();
            }
        }

        private Func<PostOffice, IComparable>[] _sortComparisons;
        
        public Func<PostOffice, IComparable>[] SortComparisons
        {
            get
            {
                return _sortComparisons ?? (_sortComparisons = new Func<PostOffice, IComparable>[]
                {
                    null,
                    (fir) => int.Parse(fir.ClassOPS),
                    (fir) => fir.ShortName,
                    (fir) => GetDist(fir)
                });
            }
        }
        
        

        private Func<PostOffice, bool>[] GetFilterPredicates(string filter)
        {
            return new Func<PostOffice, bool>[]
            {
                (arg) => true,//None filter
                (arg) => arg.TypeOPS.ToLower() == filter.ToLower(),
                (arg) => arg.Location.AdmArea.ToLower() == filter.ToLower()

            };
        }
        

        private int _prefixCount;

        public int PrefixCount
        {
            get => _prefixCount;

            set
            {
                if (value != _prefixCount)
                {
                    _prefixCount = value;
                    OnPropertyChanged();
                    OnPropertyChanged("PostOfficesPrefix");
                }
            }
        }

        //private ObservableCollection<PostOffice> _postOfficesPrefix;

        public ObservableCollection<PostOffice> PostOfficesPrefix
        {
            get
            {
                return SortComparisons[_sortPredicateIndex] != null ? new ObservableCollection<PostOffice>(PostOffices.
                    Take(PrefixCount).
                    Where(GetFilterPredicates(FilterStr)[_filterPredicateIndex]).
                    OrderBy(SortComparisons[_sortPredicateIndex])) :
                    new ObservableCollection<PostOffice>(PostOffices.Take(PrefixCount).
                    Where(GetFilterPredicates(FilterStr)[_filterPredicateIndex]));

            }

        }

        private Action _onInvalidCoordsDelegate;

        public Action OnInvalidCoordsDelegate
        {
            get
            {
                return _onInvalidCoordsDelegate ?? (_onInvalidCoordsDelegate = new Action( () =>
                {
                    MessageBox.Show("Invalid coords", "Wrong format");
                }));
            }
        }

        private ObservableCollection<PostOffice> _postOffices;

        public ObservableCollection<PostOffice> PostOffices
        {
            get => _postOffices;

            set
            {
                _postOffices = value;


                _postOffices.ToList().ForEach(p => p.Location.Coords.IncorrectCoordsEntered += () =>
                {
                    OnInvalidCoordsDelegate?.Invoke();
                });
                _postOffices.CollectionChanged += (s, e) => {
                    if (e.NewItems != null)
                    {
                        foreach (var el in e.NewItems)
                        {
                            (el as PostOffice).Location.Coords.IncorrectCoordsEntered += () =>
                            {
                                OnInvalidCoordsDelegate?.Invoke();
                            };
                        }
                    }

                    OnPropertyChanged("PostOffices");

                };
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
                        List<PostOffice> fetchResult;
                        bool success;
                        (success, fetchResult) = IOHelper.ReadData(filePath);
                        if (success)
                        {
                            this.PostOffices = new ObservableCollection<PostOffice>(fetchResult);
                            OnPropertyChanged("PostOfficesPrefix");
                        }
                    }
                }));
            }
        }

        private RelayCommand _sortByCommand;

        public RelayCommand SortByCommand
        {
            get
            {
                return _sortByCommand ?? (_sortByCommand = new RelayCommand(obj =>
                {
                    int index = (int)obj;
                    _sortPredicateIndex = index;
                    string[] arr = new string[] { "None", "ClassOPS", "ShortName"};
                    SortByText = "Sort By" + Environment.NewLine + arr[index];
                    OnPropertyChanged("PostOfficesPrefix");
                }));
            }
        }

        private RelayCommand _filterByCommand;

        public RelayCommand FilterByCommand
        {
            get
            {
                return _filterByCommand ?? (_filterByCommand = new RelayCommand(obj =>
                {
                    int index = (int)obj;
                    _filterPredicateIndex = index;
                    string[] arr = new string[] { "None", "TypeOPS", "AdmArea" };
                    FilterByText = "Filter By" + Environment.NewLine + arr[index];
                    OnPropertyChanged("PostOfficesPrefix");
                }));
            }
        }

        private RelayCommand _openFilterSettingCommand;

        public RelayCommand OpenFilterSettingsCommand
        {
            get
            {
                return _openFilterSettingCommand ?? (_openFilterSettingCommand = new RelayCommand(obj =>
                {
                    int index = (int)obj;

                    FilterSettingsWindow w = new FilterSettingsWindow(index, this);
                    w.Show();
                }));
            }
        }

        private RelayCommand _saveToFileCommand;

        public RelayCommand SaveToFileCommand
        {
            get
            {
                return _saveToFileCommand ?? (_saveToFileCommand = new RelayCommand(obj => 
                {
                   
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.DefaultExt = "csv";
                    if (dialog.ShowDialog() == true)
                    {
                        string filePath = dialog.FileName;
                        IOHelper.WriteData(this.PostOfficesPrefix, filePath, false);
                    }
                }));
            }
        }

        private RelayCommand _rewriteFileCommand;

        public RelayCommand RewriteFileCommand
        {
            get
            {
                return _rewriteFileCommand ?? (_rewriteFileCommand = new RelayCommand(obj =>
                {
                    bool append = (bool)obj;

                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "Excel Files|*.csv;";
                    dialog.DefaultExt = ".csv";

                    if (dialog.ShowDialog() == true)
                    {
                        IOHelper.WriteData(PostOfficesPrefix, dialog.FileName, append);
                    }

                }));
            }
        }

        private RelayCommand _findClosestCommand;

        public RelayCommand FindClosestCommand
        {
            get
            {
                return _findClosestCommand ?? (_findClosestCommand = new RelayCommand(obj =>
                {
                    FindClosestWindow w = new FindClosestWindow(this);
                    w.Show();
                }));
            }
        }

        private Models.Point _center;

        private double GetDist(PostOffice p1)
        {
            //Haversine formula [https://en.wikipedia.org/wiki/Haversine_formula]
            double EarthRadius = 6371;
            double deltaLat = DegreesToRadians(p1.Location.Coords.X - _center.X);
            double deltaLon = DegreesToRadians(p1.Location.Coords.Y - _center.Y);
            double hav = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                         Math.Cos(DegreesToRadians(p1.Location.Coords.X)) * Math.Cos(DegreesToRadians(_center.X)) *
                         Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double ans = EarthRadius * 2 * Math.Atan2(Math.Sqrt(hav), Math.Sqrt(1 - hav));
            return ans;
        }

        private double DegreesToRadians(double deg)
        {
            return deg * Math.PI / 180;

        }

        public void SetCenterPoint(Models.Point center)
        {
            _center = center;
            _sortPredicateIndex = 3;
            SortByText = $"Sort By\nDist To:({center.X.ToString("F6")}, {center.Y.ToString("F6")})";
            OnPropertyChanged("PostOfficesPrefix");
        }

        private string _sortByText = "Sort By \nNone";

        public string SortByText
        {
            get => _sortByText;

            set
            {
                _sortByText = value;
                OnPropertyChanged();
            }
        }

        private string _filterByText = "Filter By\nNone";

        public string FilterByText
        {
            get => _filterByText;

            set
            {
                _filterByText = value;
                OnPropertyChanged();
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
