using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using PostOfficesDataDisplayer.Utils;
using PostOfficesDataDisplayer.Models;

namespace PostOfficesDataDisplayer.ViewModels
{
    public class FindClosestViewModel : INotifyPropertyChanged
    {
        private double _x;

        private double _y;

        private bool _isXCoordOk;

        private bool _isYCoordOk;

        private string _xCoordStr;

        private void UpdateApplyButton()
        {
            IsApplyButtonEnabled = _isYCoordOk && _isXCoordOk;
        }

        /*public string XCoordStr
        {
            get => _xCoordStr;

            set
            {
                double helper;
                if (!double.TryParse(value, out helper) && value.Length > 0 || value.Contains(",") || helper < -90 || helper > 90)
                {
                    MessageBox.Show("Invalid value for X coordinate");
                    //_isXCoordOk = false;
                }
                else
                {
                    _x = helper;
                    _isXCoordOk = value.Length > 0;
                    _xCoordStr = value;
                    OnPropertyChanged();
                }
                UpdateApplyButton();
            }
        }

        private string _yCoordStr;

        public string YCoordStr
        {
            get => _yCoordStr;

            set
            {
                double helper;
                if (!double.TryParse(value, out helper) && value.Length > 0 || value.Contains(",") || helper < -90 || helper > 90)
                {
                    MessageBox.Show("Invalid value for Y coordinate");
                    //_isYCoordOk = false;
                }
                else
                {
                    _y = helper;
                    _yCoordStr = value;
                    _isYCoordOk = value.Length > 0 ;
                    OnPropertyChanged();
                }
                UpdateApplyButton();
            }
        }*/



        private Models.Point _coords;

        public Models.Point Coords
        {
            get => _coords;

            set
            {
                _coords = value;
                _coords.PropertyChanged += (s, e) =>
                {
                    this._isYCoordOk = _coords.YCoordStr.Length > 0;
                    this._isXCoordOk = _coords.XCoordStr.Length > 0;
                    this.UpdateApplyButton();
                    this.OnPropertyChanged("Coords");
                };
                OnPropertyChanged();
            }
        }

        private bool _isApplyButtonEnabled;

        public bool IsApplyButtonEnabled
        {
            get => _isApplyButtonEnabled;

            set
            {
                _isApplyButtonEnabled = value;

                if (value)
                {
                    ApplyButtonColor = Brushes.Aqua;
                }
                else
                {
                    ApplyButtonColor = Brushes.LightGray;
                }

                OnPropertyChanged();
            }
        }

        private Brush _applyButtonColor = Brushes.LightGray;

        public Brush ApplyButtonColor
        {
            get => _applyButtonColor;

            set
            {
                _applyButtonColor = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _applyButtonCommand;

        public RelayCommand ApplyButtonCommand
        {
            get
            {
                return _applyButtonCommand ?? (_applyButtonCommand = new RelayCommand(obj =>
                {
                    OnChosenCoordinates();
                    NotifyToClose?.Invoke();
                }));
            }
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChosenCoordinates()
        {
            ChosenCoordinates?.Invoke(Coords.X, Coords.Y);
        }

        public FindClosestViewModel()
        {
            this.Coords = new Models.Point("", "");
        }

        public event Action<double, double> ChosenCoordinates;

        public event Action NotifyToClose;
    }
}
