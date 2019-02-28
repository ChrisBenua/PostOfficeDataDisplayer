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
using PostOfficesDataDisplayer.ViewModels;
using PostOfficesDataDisplayer.Models;

namespace PostOfficesDataDisplayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private PostOfficeDisplayerViewModel viewModel;
        public MainWindow()
        {
            viewModel = new PostOfficeDisplayerViewModel();
            InitializeComponent();

            mAddButton.Command = viewModel.AddCommand;
            mDeleteButton.Command = viewModel.DeleteCommand;

            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserDeleteRows = false;
            dataGrid.CanUserReorderColumns = false;
            dataGrid.CanUserSortColumns = false;
            dataGrid.CanUserResizeColumns = true;
            

            dataGrid.AutoGenerateColumns = false;

            dataGrid.ItemsSource = viewModel.PostOffices;

            for (int i = 0; i < PostOffice.PropertieNames.Length; ++i)
            {
                DataGridTemplateColumn column = new DataGridTemplateColumn()
                {
                    CellTemplate = getTextColumnTemplate(i)
                };

                column.Width = 40;

                column.Header = PostOffice.PropertieNames[i].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last();
                dataGrid.Columns.Add(column);
            }
            
            //(new TextBox()).LostFocus += MainWindow_LostFocus;
        }

        private void OnCoordsTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            PostOffice office = ((FrameworkElement)sender).DataContext as PostOffice;

            if (!office.Location.Coords.Valid())
            {
                MessageBox.Show("Invalid Coords, please, correct them", "Wrong format");
            }
        }

        private void IntNumberTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox current = (sender as TextBox);
            int helper;
            if (!int.TryParse(current.Text + e.Text, out helper) || (current.Text + e.Text).Length > PostOfficeDisplayerViewModel.MaxLenForNumberColumns)
            {
                e.Handled = true;
                MessageBox.Show("Only numbers allowed", "Wrong format");
            }
        }

        private void DoubleNumberTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox current = (sender as TextBox);
            double helper;
            if (e.Text == ".")
            {
                if (!double.TryParse(current.Text + e.Text, out helper) || (current.Text + e.Text).Length > PostOfficeDisplayerViewModel.MaxLenForNumberColumns)
                {
                    e.Handled = true;
                    MessageBox.Show("Only numbers allowed", "Wrong format");
                }
            }
        }

        private void StringTextBoxPreviewInput(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text.Length + (sender as TextBox).Text.Length) > PostOfficeDisplayerViewModel.MaxLenForStringColumns)
            {
                e.Handled = true;
            }
        }

        public DataTemplate getTextColumnTemplate(int index)
        {
            DataTemplate template = new DataTemplate();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TextBox));
            factory.SetValue(TextBox.FontSizeProperty, 15.0);
            factory.SetValue(TextBox.BackgroundProperty, Brushes.Transparent);
            factory.SetValue(TextBox.IsEnabledProperty, true);
            factory.SetValue(TextBox.BorderBrushProperty, Brushes.Transparent);
            factory.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));

            if (PostOfficeDisplayerViewModel.IntegerColumns.Contains(index))
            {
                factory.AddHandler(TextBox.PreviewTextInputEvent, new TextCompositionEventHandler(IntNumberTextBoxPreviewTextInput));
            }
            else if (PostOfficeDisplayerViewModel.DoubleColumns.Contains(index))
            {
                if (PostOffice.PropertieNames[index].Contains("CoordStr"))
                {
                    factory.AddHandler(TextBox.LostFocusEvent, new RoutedEventHandler(OnCoordsTextBoxLostFocus));
                    factory.SetBinding(TextBox.TextProperty, new Binding()
                    {
                        Path = new PropertyPath(PostOffice.PropertieNames[index]),
                        Mode = BindingMode.TwoWay,
                        NotifyOnSourceUpdated = true,
                        NotifyOnTargetUpdated = true,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        StringFormat = "G4"
                    });
                }
                {
                    factory.AddHandler(TextBox.PreviewTextInputEvent, new TextCompositionEventHandler(DoubleNumberTextBoxPreviewTextInput));
                }
            }
            else
            {
                factory.AddHandler(TextBox.PreviewTextInputEvent, new TextCompositionEventHandler(StringTextBoxPreviewInput));
            }

            if (!PostOffice.PropertieNames[index].Contains("CoordStr"))
            {

                factory.SetBinding(TextBox.TextProperty, new Binding()
                {
                    Path = new PropertyPath(PostOffice.PropertieNames[index]),
                    Mode = BindingMode.TwoWay,
                    NotifyOnSourceUpdated = true,
                    NotifyOnTargetUpdated = true,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });
            }

            template.VisualTree = factory;
            return template;
        }
    }
}
