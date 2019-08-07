using SalesOrderWpf.Domain.ViewModels;
using SalesOrderWpf.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SalesOrderWpf.UI
{
    /// <summary>
    /// Lógica interna para OrdersForm.xaml
    /// </summary>
    public partial class OrdersForm : Window
    {
        public ObservableCollection<LineInput> Lines { get; set; }

        public OrdersForm()
        {
            InitializeComponent();
            Lines = new ObservableCollection<LineInput>();
            gridLines.ItemsSource = Lines;
            SignEvents();
        }

        private void SignEvents()
        {
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FormatGrid();
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void FormatGrid()
        {
            gridLines.SetHeader("Id", "Código");
            gridLines.SetHeader("OrderId", "Pedido");
            gridLines.SetHeader("ItemCode", "Código Item");
            gridLines.SetHeader("ItemName", "Descrição");
            gridLines.SetHeader("Price", "Preço");
            gridLines.SetHeader("Quantity", "Quantidade");
            gridLines.SetHeader("Total", "Total");
            gridLines.IsReadOnly = true;
        }

        private void CmdOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var est = Lines;
                Lines.Add(new LineInput { ItemCode = "I0002", ItemName = "Clip", Price = 13.22m, Quantity = 5, Total = 57.86m });
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }
    }
}
