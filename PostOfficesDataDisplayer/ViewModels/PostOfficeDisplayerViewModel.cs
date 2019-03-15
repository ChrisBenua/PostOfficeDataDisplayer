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
using System.Diagnostics;

namespace PostOfficesDataDisplayer.ViewModels
{
    /// <summary>
    /// Post office displayer view model.
    /// </summary>
    public class PostOfficeDisplayerViewModel: INotifyPropertyChanged
    {
        /// <summary>
        /// The integer columns.
        /// </summary>
        public static readonly int[] IntegerColumns = new int[] { 0, 3, 13, 18, 21 };

        /// <summary>
        /// The double columns.
        /// </summary>
        public static readonly int[] DoubleColumns = new int[] {19, 20};

        /// <summary>
        /// The phones columns.
        /// </summary>
        public static readonly int[] PhonesColumns = new int[] {8, 9, 10};

        /// <summary>
        /// The max length for double columns.
        /// </summary>
        public static readonly int MaxLenForDoubleColumns = 50;

        /// <summary>
        /// The max length for int columns.
        /// </summary>
        public static readonly int MaxLenForIntColumns = 10;

        /// <summary>
        /// The max length for string columns.
        /// </summary>
        public static readonly int MaxLenForStringColumns = 1000;

        /// <summary>
        /// The index of the filter predicate.
        /// </summary>
        private int _filterPredicateIndex = 0;

        /// <summary>
        /// The index of the sort predicate.
        /// </summary>
        private int _sortPredicateIndex = 0;

        /// <summary>
        /// The filter string.
        /// </summary>
        private string _filterStr;

        /// <summary>
        /// Gets or sets the filter string.
        /// </summary>
        /// <value>The filter string.</value>
        public string FilterStr
        {
            get => _filterStr;
            set
            {
                _filterStr = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The sort comparisons.
        /// </summary>
        private Func<PostOffice, IComparable>[] _sortComparisons;

        /// <summary>
        /// Gets the sort comparisons.
        /// </summary>
        /// <value>The sort comparisons.</value>
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
        
        
        /// <summary>
        /// Gets the filter predicates.
        /// </summary>
        /// <returns>The filter predicates.</returns>
        /// <param name="filter">Filter.</param>
        private Func<PostOffice, bool>[] GetFilterPredicates(string filter)
        {
            return new Func<PostOffice, bool>[]
            {
                (arg) => true,//None filter
                (arg) => arg.TypeOPS.ToLower() == filter.ToLower(),
                (arg) => arg.Location.AdmArea.ToLower() == filter.ToLower()

            };
        }
        
        /// <summary>
        /// The number of items to be shown.
        /// </summary>
        private int _prefixCount;

        /// <summary>
        /// Gets or sets the prefix count.
        /// </summary>
        /// <value>The prefix count.</value>
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

        /// <summary>
        /// Gets the post offices prefix.
        /// </summary>
        /// <value>The post offices prefix.</value>
        public IEnumerable<PostOffice> PostOfficesPrefix
        {
            get
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var ans = SortComparisons[_sortPredicateIndex] != null ? PostOffices.
                    Take(PrefixCount).
                    Where(GetFilterPredicates(FilterStr)[_filterPredicateIndex]).
                    OrderBy(SortComparisons[_sortPredicateIndex]) 
                    :
                    
                    PostOffices.Take(PrefixCount).
                    Where(GetFilterPredicates(FilterStr)[_filterPredicateIndex]);
                stopWatch.Stop();
                Console.WriteLine(stopWatch.ElapsedMilliseconds);
                return ans;

            }

        }

        /// <summary>
        /// The on invalid coords delegate.
        /// </summary>
        private Action _onInvalidCoordsDelegate;

        /// <summary>
        /// Gets the on invalid coords delegate.
        /// </summary>
        /// <value>The on invalid coords delegate.</value>
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

        /// <summary>
        /// The post offices.
        /// </summary>
        private ObservableCollection<PostOffice> _postOffices;

        /// <summary>
        /// Gets or sets the post offices.
        /// </summary>
        /// <value>The post offices.</value>
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

        /// <summary>
        /// The selected office.
        /// </summary>
        private PostOffice _selectedOffice;


        /// <summary>
        /// Gets or sets the selected office.
        /// </summary>
        /// <value>The selected office.</value>
        public PostOffice SelectedOffice
        {
            get => _selectedOffice;

            set
            {
                _selectedOffice = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The delete command.
        /// </summary>
        private RelayCommand _deleteCommand;

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>The delete command.</value>
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

        /// <summary>
        /// The add command.
        /// </summary>
        private RelayCommand _addCommand;

        /// <summary>
        /// Gets the add command.
        /// </summary>
        /// <value>The add command.</value>
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

        /// <summary>
        /// The open file command.
        /// </summary>
        private RelayCommand _openFileCommand;

        /// <summary>
        /// Gets the open file command.
        /// </summary>
        /// <value>The open file command.</value>
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
                        try
                        {
                            (success, fetchResult) = IOHelper.ReadData(filePath);
                            if (success)
                            {
                                this.PostOffices = new ObservableCollection<PostOffice>(fetchResult);
                                OnPropertyChanged("PostOfficesPrefix");
                            }
                        }
                        catch (System.IO.IOException ex)
                        {
                            MessageBox.Show(ex.Message, "Error while reading from file");
                        }
                        catch (System.UnauthorizedAccessException ex)
                        {
                            MessageBox.Show(ex.Message, "Error in accessing/creating a file");
                        }
                        catch (System.ArgumentException ex)
                        {
                            MessageBox.Show(ex.Message, "Error in file path");
                        }
                        catch (System.Security.SecurityException ex)
                        {
                            MessageBox.Show(ex.Message, "Security Violation");
                        }
                    }
                }));
            }
        }

        /// <summary>
        /// The sort by command.
        /// </summary>
        private RelayCommand _sortByCommand;

        /// <summary>
        /// Gets the sort by command.
        /// </summary>
        /// <value>The sort by command.</value>
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

        /// <summary>
        /// The filter by command.
        /// </summary>
        private RelayCommand _filterByCommand;

        /// <summary>
        /// Gets the filter by command.
        /// </summary>
        /// <value>The filter by command.</value>
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

        /// <summary>
        /// The open filter setting command.
        /// </summary>
        private RelayCommand _openFilterSettingCommand;

        /// <summary>
        /// Gets the open filter settings command.
        /// </summary>
        /// <value>The open filter settings command.</value>
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

        /// <summary>
        /// The save to file command.
        /// </summary>
        private RelayCommand _saveToFileCommand;

        /// <summary>
        /// Gets the save to file command.
        /// </summary>
        /// <value>The save to file command.</value>
        public RelayCommand SaveToFileCommand
        {
            get
            {
                return _saveToFileCommand ?? (_saveToFileCommand = new RelayCommand(obj => 
                {
                   
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.DefaultExt = "csv";
                    dialog.Filter = "Excel Files|*.csv;";
                    if (dialog.ShowDialog() == true)
                    {
                        string filePath = dialog.FileName;
                        IOHelper.WriteData(this.PostOfficesPrefix, filePath, false);
                    }
                }));
            }
        }

        /// <summary>
        /// The rewrite file command.
        /// </summary>
        private RelayCommand _rewriteFileCommand;

        /// <summary>
        /// Gets the rewrite file command.
        /// </summary>
        /// <value>The rewrite file command.</value>
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
                        try
                        {
                            IOHelper.WriteData(PostOfficesPrefix, dialog.FileName, append);
                        }
                        catch (System.IO.IOException ex)
                        {
                            MessageBox.Show(ex.Message, "Error in writing to file");
                        }
                        catch (System.UnauthorizedAccessException ex)
                        {
                            MessageBox.Show(ex.Message, "Error in accessing/creating a file");
                        }
                        catch (System.ArgumentException ex)
                        {
                            MessageBox.Show(ex.Message, "Error in file path");
                        }
                    }

                }));
            }
        }

        /// <summary>
        /// The find closest command.
        /// </summary>
        private RelayCommand _findClosestCommand;

        /// <summary>
        /// Gets the find closest command.
        /// </summary>
        /// <value>The find closest command.</value>
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

        /// <summary>
        /// The open on map command.
        /// </summary>
        private RelayCommand _openOnMapCommand;

        /// <summary>
        /// Gets the open on map command.
        /// </summary>
        /// <value>The open on map command.</value>
        public RelayCommand OpenOnMapCommand
        {
            get
            {
                return _openOnMapCommand ?? (_openOnMapCommand = new RelayCommand(obj =>
                {
                    GEOJsonPostOfficeCollection collection = new GEOJsonPostOfficeCollection(this.PostOfficesPrefix);
                    URLManager.OpenURL(collection.EscapedJSONString());
                }));
            }
        }

        /// <summary>
        /// The save as geo json command.
        /// </summary>
        private RelayCommand _saveAsGeoJsonCommand;

        /// <summary>
        /// Gets the save as geo json command.
        /// </summary>
        /// <value>The save as geo json command.</value>
        public RelayCommand SaveAsGeoJsonCommand
        {
            get
            {
                return _saveAsGeoJsonCommand ?? (_saveAsGeoJsonCommand = new RelayCommand(obj =>
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.DefaultExt = ".geojson";

                    if (dialog.ShowDialog() == true)
                    {
                        try
                        {
                            IOHelper.WriteGeoJson(dialog.FileName, this.PostOfficesPrefix);
                        }
                        catch (System.IO.IOException ex)
                        {
                            MessageBox.Show(ex.Message, "Error in writing to file");
                        }
                        catch (System.UnauthorizedAccessException ex)
                        {
                            MessageBox.Show(ex.Message, "Error in accessing/creating a file");
                        }
                        catch (System.ArgumentException ex)
                        {
                            MessageBox.Show(ex.Message, "Error in file path");
                        }
                    }
                }));
            }
        }

        /// <summary>
        /// The center.
        /// </summary>
        private Models.Point _center;

        /// <summary>
        /// Gets the dist.
        /// </summary>
        /// <returns>The dist.</returns>
        /// <param name="p1">P1.</param>
        private double GetDist(PostOffice p1)
        {
            //Haversine formula [https://en.wikipedia.org/wiki/Haversine_formula]
            return p1.Location.DistTo(_center);
        }

        /// <summary>
        /// Sets the center point.
        /// </summary>
        /// <param name="center">Center.</param>
        public void SetCenterPoint(Models.Point center)
        {
            _center = center;
            _sortPredicateIndex = 3;
            SortByText = $"Sort By\nDist To:({center.X.ToString("F6")}, {center.Y.ToString("F6")})";
            OnPropertyChanged("PostOfficesPrefix");
        }

        /// <summary>
        /// The sort by text.
        /// </summary>
        private string _sortByText = "Sort By \nNone";

        /// <summary>
        /// Gets or sets the sort by text.
        /// </summary>
        /// <value>The sort by text.</value>
        public string SortByText
        {
            get => _sortByText;

            set
            {
                _sortByText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The filter by text.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:PostOfficesDataDisplayer.ViewModels.PostOfficeDisplayerViewModel"/> class.
        /// </summary>
        public PostOfficeDisplayerViewModel()
        {
            PostOffices = new ObservableCollection<PostOffice>();
        }

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
