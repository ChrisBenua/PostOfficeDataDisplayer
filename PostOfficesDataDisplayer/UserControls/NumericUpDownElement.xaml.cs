using System;
using System.Collections.Generic;
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
using PostOfficesDataDisplayer.UserControls.UserControlsViewModel;

namespace PostOfficesDataDisplayer.UserControls
{
    /// <summary>
    /// Interaction logic for NumericUpDownElement.xaml
    /// </summary>
    public partial class NumericUpDownElement : UserControl
    {

        public readonly static DependencyProperty MinimumValueProperty = DependencyProperty.Register("Minimum", typeof(int), 
            typeof(NumericUpDownElement), new UIPropertyMetadata(0, ValueChanged));
        public readonly static DependencyProperty MaximumValueProperty = DependencyProperty.Register("Maximum", typeof(int), 
            typeof(NumericUpDownElement), new UIPropertyMetadata(100, ValueChanged));
        public readonly static DependencyProperty InitialValueProperty = DependencyProperty.Register("InitialValue", typeof(int),
            typeof(NumericUpDownElement), new UIPropertyMetadata(0, ValueChanged));
        

        public NumericUpDownViewModel ViewModel { get; private set; }

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NumericUpDownElement;

            control.ViewModel.MaxValue = (int)control.GetValue(MaximumValueProperty);
            control.ViewModel.MinValue = (int)control.GetValue(MinimumValueProperty);                
        }

        public int Maximum
        {
            get
            {
                return (int)GetValue(MaximumValueProperty);
            }
            set
            {
                SetValue(MaximumValueProperty, value);
            }
        }

        //public int Maximum => ;

        public int Minimum
        {
            get
            {
                return (int)GetValue(MinimumValueProperty);
            }

            set
            {
                SetValue(MinimumValueProperty, value);
            }
        }

        public int InitialValue
        {
            get
            {
                return (int)GetValue(InitialValueProperty);
            }

            set
            {
                SetValue(InitialValueProperty, value);
            }
        }

        public NumericUpDownElement()
        {
            InitializeComponent();
            ViewModel = new NumericUpDownViewModel(Minimum, Maximum, InitialValue);


            valueTextBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Source = ViewModel,
                Path = new PropertyPath("Text"),
                Mode = BindingMode.TwoWay,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            increaseButton.Command = ViewModel.IncreaseCommand;
            decreaseButton.Command = ViewModel.DecreaseCommand;

            ViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Text")
                {
                    valueTextBox.CaretIndex = ViewModel.Text.Length;
                }
            };
        }
    }
}
