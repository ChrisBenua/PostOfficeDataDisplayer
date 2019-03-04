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

namespace PostOfficesDataDisplayer.Views
{
    /// <summary>
    /// Interaction logic for FindClosestWindow.xaml
    /// </summary>
    public partial class FindClosestWindow : Window
    {
        private FindClosestViewModel viewModel;

        public FindClosestWindow(PostOfficeDisplayerViewModel postOfficeViewModel)
        {
            viewModel = new FindClosestViewModel();
            viewModel.ChosenCoordinates += (xCoord, yCoord) => postOfficeViewModel.SetCenterPoint(new Models.Point(xCoord, yCoord));
            InitializeComponent();

            mApplyButton.SetBinding(Button.IsEnabledProperty, new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath("IsApplyButtonEnabled"),
                NotifyOnSourceUpdated = true,
            });

            mApplyButton.SetBinding(Button.BackgroundProperty, new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath("ApplyButtonColor"),
                NotifyOnSourceUpdated = true
            });

            mxCoordTextBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Source = viewModel.Coords,
                Path = new PropertyPath("XCoordStr"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            myCoordTextBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Source = viewModel.Coords,
                Path = new PropertyPath("YCoordStr"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            viewModel.NotifyToClose += () => this.Close();

            mApplyButton.Command = viewModel.ApplyButtonCommand;
        }
    }
}
