﻿using System;
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
        /// <summary>
        /// The minimum value property.
        /// </summary>
        public readonly static DependencyProperty MinimumValueProperty = DependencyProperty.Register("Minimum", typeof(int), 
            typeof(NumericUpDownElement), new UIPropertyMetadata(0, ValueChanged));

        /// <summary>
        /// The maximum value property.
        /// </summary>
        public readonly static DependencyProperty MaximumValueProperty = DependencyProperty.Register("Maximum", typeof(int), 
            typeof(NumericUpDownElement), new UIPropertyMetadata(100, ValueChanged));

        /// <summary>
        /// The initial value property.
        /// </summary>
        public readonly static DependencyProperty InitialValueProperty = DependencyProperty.Register("InitialValue", typeof(int),
            typeof(NumericUpDownElement), new UIPropertyMetadata(0, ValueChanged));
        
        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public NumericUpDownViewModel ViewModel { get; private set; }

        /// <summary>
        /// Values the changed.
        /// </summary>
        /// <param name="d">D.</param>
        /// <param name="e">E.</param>
        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NumericUpDownElement;

            control.ViewModel.MaxValue = (int)control.GetValue(MaximumValueProperty);
            control.ViewModel.MinValue = (int)control.GetValue(MinimumValueProperty);                
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
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

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
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

        /// <summary>
        /// Gets or sets the initial value.
        /// </summary>
        /// <value>The initial value.</value>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.UserControls.NumericUpDownElement"/> class.
        /// </summary>
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
