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
            Loaded += Window_Loaded;
            cmdCancel.Click += CmdCancel_Click;
            cmdOk.Click += CmdOk_Click;
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

        private void Save()
        {
            var order = new OrderInput();
            order.CardCode = txtCardCode.Text;
            order.CardName = txtCardName.Text;
            order.Lines = Lines;
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

        private void CmdOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var est = Lines;
                Lines.Add(new LineInput { ItemCode = "I0002", ItemName = "Clip", Price = 13.22m, Quantity = 5 });
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void CmdCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void BtnAddLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void BtnUpdateLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void BtnRemoveLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }
    }
}
