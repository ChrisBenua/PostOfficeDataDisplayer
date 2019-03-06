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
using System.Windows.Shapes;
using PostOfficesDataDisplayer.ViewModels;

namespace PostOfficesDataDisplayer
{
    /// <summary>
    /// Interaction logic for FilterSettingsWindow.xaml
    /// </summary>
    public partial class FilterSettingsWindow : Window
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.FilterSettingsWindow"/> class.
        /// </summary>
        /// <param name="filterIndex">Filter index.</param>
        /// <param name="viewModel">View model.</param>
        public FilterSettingsWindow(int filterIndex, PostOfficeDisplayerViewModel viewModel)
        {
            InitializeComponent();

            mFilterStrTextBox.TextChanged += (s, e) =>
            {
                if ((s as TextBox).Text.Length > 0)
                {
                    mApplyButton.Background = Brushes.Aqua;
                }
                else
                {
                    mApplyButton.Background = Brushes.LightGray;
                }
            };

            mApplyButton.Click += (s, e) =>
            {
                viewModel.FilterStr = this.mFilterStrTextBox.Text;
                this.Close();
                viewModel.FilterByCommand.Execute(filterIndex);
            };
        }
    }
}
