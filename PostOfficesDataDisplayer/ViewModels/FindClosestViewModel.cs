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
    /// <summary>
    /// Find closest window view model.
    /// </summary>
    public class FindClosestViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The x.
        /// </summary>
        private double _x;

        /// <summary>
        /// The y.
        /// </summary>
        private double _y;

        /// <summary>
        /// The is XCoord ok.
        /// </summary>
        private bool _isXCoordOk;

        /// <summary>
        /// The is YCoord ok.
        /// </summary>
        private bool _isYCoordOk;

        /// <summary>
        /// The  x coordinate string.
        /// </summary>
        private string _xCoordStr;

        /// <summary>
        /// Updates the apply button.
        /// </summary>
        private void UpdateApplyButton()
        {
            IsApplyButtonEnabled = _isYCoordOk && _isXCoordOk;
        }

        /// <summary>
        /// The coords.
        /// </summary>
        private Models.Point _coords;

        /// <summary>
        /// Gets or sets the coords.
        /// </summary>
        /// <value>The coords.</value>
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

        /// <summary>
        /// The is apply button enabled.
        /// </summary>
        private bool _isApplyButtonEnabled;

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:PostOfficesDataDisplayer.ViewModels.FindClosestViewModel"/> is apply button enabled.
        /// </summary>
        /// <value><c>true</c> if is apply button enabled; otherwise, <c>false</c>.</value>
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

        /// <summary>
        /// The color of the apply button.
        /// </summary>
        private Brush _applyButtonColor = Brushes.LightGray;

        /// <summary>
        /// Gets or sets the color of the apply button.
        /// </summary>
        /// <value>The color of the apply button.</value>
        public Brush ApplyButtonColor
        {
            get => _applyButtonColor;

            set
            {
                _applyButtonColor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The apply button command.
        /// </summary>
        private RelayCommand _applyButtonCommand;

        /// <summary>
        /// Gets the apply button command.
        /// </summary>
        /// <value>The apply button command.</value>
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

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Ons the chosen coordinates.
        /// </summary>
        public void OnChosenCoordinates()
        {
            ChosenCoordinates?.Invoke(Coords.X, Coords.Y);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.ViewModels.FindClosestViewModel"/> class.
        /// </summary>
        public FindClosestViewModel()
        {
            this.Coords = new Models.Point("", "");
        }

        /// <summary>
        /// Occurs when chosen coordinates.
        /// </summary>
        public event Action<double, double> ChosenCoordinates;

        /// <summary>
        /// Occurs when notify to close.
        /// </summary>
        public event Action NotifyToClose;
    }
}
