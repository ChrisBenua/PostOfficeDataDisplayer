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
            typeof(NumericUpDownElement), new UIPropertyMetadata(0));
        public readonly static DependencyProperty MaximumValueProperty = DependencyProperty.Register("Maximum", typeof(int), 
            typeof(NumericUpDownElement), new UIPropertyMetadata(100));
        public readonly static DependencyProperty InitialValueProperty = DependencyProperty.Register("InitialValue", typeof(int),
            typeof(NumericUpDownElement), new UIPropertyMetadata(0));

        private NumericUpDownViewModel viewModel;

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
            viewModel = new NumericUpDownViewModel(Minimum, Maximum, InitialValue);
            InitializeComponent();

            valueTextBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath("Text"),
                Mode = BindingMode.TwoWay,
                NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            increaseButton.Command = viewModel.IncreaseCommand;
            decreaseButton.Command = viewModel.DecreaseCommand;

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Text")
                {
                    valueTextBox.CaretIndex = viewModel.Text.Length;
                }
            };
        }
    }
}
