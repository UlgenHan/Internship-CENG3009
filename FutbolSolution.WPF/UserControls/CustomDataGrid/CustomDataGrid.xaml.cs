
using FutbolSolution.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace FutbolSolution.WPF.UserControls.CustomDataGrid
{
    /// <summary>
    /// Interaction logic for CustomDataGrid.xaml
    /// </summary>
    public partial class CustomDataGrid : UserControl
    {
        public CustomDataGrid()
        {
            InitializeComponent();
        }

        public Type ItemType
        {
            get { return (Type)GetValue(ItemTypeProperty); }
            set { SetValue(ItemTypeProperty, value); }
        }

        public static readonly DependencyProperty ItemTypeProperty =
            DependencyProperty.Register("ItemType", typeof(Type), typeof(CustomDataGrid), new PropertyMetadata(null, OnItemTypeChanged));

        private static void OnItemTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CustomDataGrid;
            control.GenerateColumns((Type)e.NewValue);
        }

        private void GenerateColumns(Type type)
        {
            if (type == null) return;

            dataGrid.Columns.Clear();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(PlayerImage))
                {
                    continue; // Skip PlayerImage property for simplicity
                }

                var column = new DataGridTextColumn
                {
                    Header = property.Name,
                    Binding = new Binding(property.Name)
                };

                dataGrid.Columns.Add(column);
            }

            // Add the action buttons column
            var actionColumn = new DataGridTemplateColumn
            {
                Header = "Actions",
                CellTemplate = (DataTemplate)FindResource("ActionTemplate")
            };

            dataGrid.Columns.Add(actionColumn);
        }

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(CustomDataGrid), new PropertyMetadata(null, OnItemsSourceChanged));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CustomDataGrid;
            if (control != null && control.dataGrid != null)
            {
                control.dataGrid.ItemsSource = e.NewValue as IEnumerable<object>;
            }
        }
    }
}
