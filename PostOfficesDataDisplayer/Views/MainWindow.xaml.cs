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
using PostOfficesDataDisplayer.ViewModels;
using PostOfficesDataDisplayer.Models;
using PostOfficesDataDisplayer.UserControls;

namespace PostOfficesDataDisplayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// The view model.
        /// </summary>
        private PostOfficeDisplayerViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PostOfficesDataDisplayer.MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            //Console.WriteLine(GC.TryStartNoGCRegion((1L << 74)));
            viewModel = new PostOfficeDisplayerViewModel();
            InitializeComponent();

            mSortByNone.Command = viewModel.SortByCommand;
            mSortByNone.CommandParameter = 0;

            mSortByClassOPS.Command = viewModel.SortByCommand;
            mSortByClassOPS.CommandParameter = 1;

            mSortByShortName.Command = viewModel.SortByCommand;
            mSortByShortName.CommandParameter = 2;

            mFilterByNone.Command = viewModel.FilterByCommand;
            mFilterByNone.CommandParameter = 0;

            mFilterByTypeOPS.Command = viewModel.OpenFilterSettingsCommand;
            mFilterByTypeOPS.CommandParameter = 1;

            mFilterByAdmArea.Command = viewModel.OpenFilterSettingsCommand;
            mFilterByAdmArea.CommandParameter = 2;

            mOpenOnMap.Command = viewModel.OpenOnMapCommand;

            mSortByDistToPoint.Command = viewModel.FindClosestCommand;

            mHintTextBox.Text = "Total entities" + Environment.NewLine + "in table";
            
            viewModel.PrefixCount = mUpDownControl.InitialValue;
            mAddButton.Command = viewModel.AddCommand;
            mDeleteButton.Command = viewModel.DeleteCommand;

            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserDeleteRows = false;
            dataGrid.CanUserReorderColumns = false;
            dataGrid.CanUserSortColumns = false;
            dataGrid.CanUserResizeColumns = true;

            dataGrid.AlternationCount = 2;
            dataGrid.RowBackground = Brushes.White;
            dataGrid.AlternatingRowBackground = Brushes.WhiteSmoke;

            mOpenFile.Command = viewModel.OpenFileCommand;
            dataGrid.AutoGenerateColumns = false;
            //dataGrid.ItemsSource = viewModel.PostOfficesPrefix;
            //dataGrid.ItemsSource = viewModel.PostOffices;

            dataGrid.SelectionMode = DataGridSelectionMode.Single;

            mSaveToNewFile.Command = viewModel.SaveToFileCommand;

            mAppendToFile.Command = viewModel.RewriteFileCommand;
            mAppendToFile.CommandParameter = true;

            mReplaceFile.Command = viewModel.RewriteFileCommand;
            mReplaceFile.CommandParameter = false;

            mSaveGEOJsonData.Command = viewModel.SaveAsGeoJsonCommand;

            mSortTextBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath("SortByText"),
                NotifyOnSourceUpdated = true
            });

            mfilterTextBox.SetBinding(TextBox.TextProperty, new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath("FilterByText"),
                NotifyOnSourceUpdated = true
            });


            mfilterTextBox.SetBinding(TextBox.ToolTipProperty, new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath("FilterStr"),
                NotifyOnSourceUpdated = true
                
            });

            for (int i = 0; i < PostOffice.PropertiesNames.Length; ++i)
            {
                DataGridTemplateColumn column = new DataGridTemplateColumn()
                {
                    CellTemplate = GetTextColumnTemplate(i)
                };

                column.Width = 80;

                column.Header = PostOffice.ColumnHeaders[i];
                dataGrid.Columns.Add(column);
            }
            dataGrid.EnableRowVirtualization = true;
            dataGrid.RowHeight = 30;
            dataGrid.SetBinding(DataGrid.SelectedItemProperty, new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath("SelectedOffice"),
                //NotifyOnSourceUpdated = true,
                NotifyOnTargetUpdated = true,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            dataGrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath("PostOfficesPrefix"),
                NotifyOnSourceUpdated = true,
            }); 

            this.mUpDownControl.ViewModel.MaxValue = 0;
            mUpDownControl.ViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Value")
                {
                    this.viewModel.PrefixCount = mUpDownControl.ViewModel.Value;
                    //this.dataGrid.ItemsSource = viewModel.PostOfficesPrefix;
                }
                
            };

            this.viewModel.PropertyChanged += (s, e) =>
            {

                if (e.PropertyName == "PostOfficesPrefix")
                {
                    this.mUpDownControl.ViewModel.MaxValue = this.viewModel.PostOffices.Count;
                    //this.dataGrid.ItemsSource = viewModel.PostOfficesPrefix;
                }

                if (e.PropertyName == "PostOffices")
                {
                    this.mDataSetSizeTextBox.Text = this.viewModel.PostOffices.Count.ToString();
                }

                if (e.PropertyName == "SelectedOffice")
                {
                    if (viewModel.SelectedOffice == null)
                    {
                        this.mDeleteButton.Background = Brushes.LightGray;
                        this.mDeleteButton.IsEnabled = false;
                    }
                    else
                    {
                        this.mDeleteButton.Background = Brushes.Red;
                        this.mDeleteButton.IsEnabled = true;
                    }
                }
            };
            
            //(new TextBox()).LostFocus += MainWindow_LostFocus;
        }

        /// <summary>
        /// Strings the text box preview input.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void StringTextBoxPreviewInput(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text.Length + (sender as TextBox).Text.Length) > PostOfficeDisplayerViewModel.MaxLenForStringColumns || e.Text.Contains("\""))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Gets the text column template.
        /// </summary>
        /// <returns>The text column template.</returns>
        /// <param name="index">Index.</param>
        public DataTemplate GetTextColumnTemplate(int index)
        {
            DataTemplate template = new DataTemplate();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TextBox));
            factory.SetValue(TextBox.FontSizeProperty, 15.0);
            factory.SetValue(TextBox.BackgroundProperty, Brushes.Transparent);
            factory.SetValue(TextBox.IsEnabledProperty, true);
            factory.SetValue(TextBox.BorderBrushProperty, Brushes.Transparent);
            factory.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));

            if (PostOfficeDisplayerViewModel.DoubleColumns.Contains(index))
            {
                if (PostOffice.PropertiesNames[index].Contains("CoordStr"))
                {
                    //factory.AddHandler(TextBox.LostFocusEvent, new RoutedEventHandler(OnCoordsTextBoxLostFocus));
                    factory.SetBinding(TextBox.TextProperty, new Binding()
                    {
                        Path = new PropertyPath(PostOffice.PropertiesNames[index]),
                        Mode = BindingMode.TwoWay,
                        NotifyOnSourceUpdated = true,
                        //NotifyOnTargetUpdated = true,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        StringFormat = "G4"
                    });
                }
                
            }
            else
            {
                factory.AddHandler(TextBox.PreviewTextInputEvent, new TextCompositionEventHandler(StringTextBoxPreviewInput));
            }

            if (!PostOffice.PropertiesNames[index].Contains("CoordStr"))
            {

                factory.SetBinding(TextBox.TextProperty, new Binding()
                {
                    Path = new PropertyPath(PostOffice.PropertiesNames[index]),
                    Mode = BindingMode.TwoWay,
                    NotifyOnSourceUpdated = true,
                    //NotifyOnTargetUpdated = true,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });
            }

            template.VisualTree = factory;
            return template;
        }
    }
}
