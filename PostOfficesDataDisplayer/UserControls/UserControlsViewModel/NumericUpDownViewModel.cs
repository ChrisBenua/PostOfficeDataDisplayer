using PostOfficesDataDisplayer.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PostOfficesDataDisplayer.UserControls.UserControlsViewModel
{
    /// <summary>
    /// Numeric up down view model.
    /// </summary>
    public class NumericUpDownViewModel: INotifyPropertyChanged
    {
        /// <summary>
        /// The minimum value.
        /// </summary>
        private int _minValue;

        /// <summary>
        /// The max value.
        /// </summary>
        private int _maxValue;

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        public int MinValue
        {
            get => _minValue;

            set
            {
                _minValue = value;
                ValidateValue();
                OnPropertyChanged();
            }

        }

        /// <summary>
        /// Gets or sets the max value.
        /// </summary>
        /// <value>The max value.</value>
        public int MaxValue
        {
            get => _maxValue;

            set
            {
                _maxValue = value;
                ValidateValue();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The text.
        /// </summary>
        private string _text;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
  
                // _text = value;
                var tempValue = new string(value.ToCharArray());
                //validation
                var arrWithNumbers = value.Where((ch) => ch >= '0' && ch <= '9').SkipWhile(a => a == '0').ToList().
                    ConvertAll<string>((ch) => new string(ch, 1));

                if (arrWithNumbers.Count != 0)
                {
                    tempValue = arrWithNumbers.Aggregate((a, b) => a + b);
                }
                else if (value.Length != 0)
                {
                    tempValue = _text;
                }
                else
                {
                    tempValue = MinValue.ToString();
                }

                _text = tempValue;

                if (!int.TryParse(_text, out _value))
                {
                    throw new ArgumentException();
                }

                Value = _value;


                OnPropertyChanged();
                
            }
        }

        /// <summary>
        /// The value.
        /// </summary>
        int _value;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                {
                    _value = value;
                    ValidateValue();

                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Validates the value.
        /// </summary>
        private void ValidateValue()
        {
            var lastVal = _value;
            _value = Math.Max(MinValue, _value);
            _value = Math.Min(MaxValue, _value);

            _text = _value.ToString();

            if (lastVal != _value)
            {
                OnPropertyChanged("Value");
            }
            OnPropertyChanged("Text");

        }

        /// <summary>
        /// The increase command.
        /// </summary>
        private RelayCommand _increaseCommand;

        /// <summary>
        /// Gets the increase command.
        /// </summary>
        /// <value>The increase command.</value>
        public RelayCommand IncreaseCommand
        {
            get
            {
                return _increaseCommand ?? (_increaseCommand = new RelayCommand(obj =>
                {
                    Value++;
                }));
            }
        }

        /// <summary>
        /// The decrease command.
        /// </summary>
        private RelayCommand _decreaseCommand;

        /// <summary>
        /// Gets the decrease command.
        /// </summary>
        /// <value>The decrease command.</value>
        public RelayCommand DecreaseCommand
        {
            get
            {
                return _decreaseCommand ?? (_decreaseCommand = new RelayCommand(obj =>
                {
                    Value--;
                }));
            }
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:PostOfficesDataDisplayer.UserControls.UserControlsViewModel.NumericUpDownViewModel"/> class.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Max value.</param>
        /// <param name="initialValue">Initial value.</param>
        public NumericUpDownViewModel(int minValue, int maxValue, int initialValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Value = initialValue;
        }

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
