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
    public class NumericUpDownViewModel: INotifyPropertyChanged
    {
        private int _minValue;
        private int _maxValue;

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

        private string _text;

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

        int _value;

        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                
                _value = value;

                ValidateValue();
                OnPropertyChanged();
            }
        }

        private void ValidateValue()
        {
            _value = Math.Max(MinValue, _value);
            _value = Math.Min(MaxValue, _value);

            _text = _value.ToString();


            OnPropertyChanged("Text");
        }

        private RelayCommand _increaseCommand;

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

        private RelayCommand _decreaseCommand;

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

        public NumericUpDownViewModel(int minValue, int maxValue, int initialValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Value = initialValue;
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
