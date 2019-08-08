using SalesOrderWpf.Business.Services;
using SalesOrderWpf.Domain.Services;
using SalesOrderWpf.Domain.ViewModels;
using SalesOrderWpf.Infra.Contexts;
using SalesOrderWpf.Infra.Repositories;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalesOrderWpf.UI
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        IOrderService _service;
        private bool _isClosed = false;

        public MainWindow()
        {
            InitializeComponent();

            StartService();
            FillGrids();
            SignEvents();
        }

        private void StartService()
        {
            var context = new SalesOrderDataContext();
            var orderRep = new OrderRepository(context);
            var lineRep = new LineRepository(context);
            _service = new OrderService(orderRep, lineRep);
        }

        private void SignEvents()
        {
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FormatGrids();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                _isClosed = true;
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private bool UserConfirm(string message)
        {
            return MessageBoxResult.Yes.Equals(FormHelper.MessageQuestion(message));
        }

        private void FormatGrids()
        {
            FormatGridOrders();
            FormatGridLines();
        }

        private void FormatGridOrders()
        {
            gridOrders.SetHeader("Id", "Código");
            gridOrders.SetHeader("CardCode", "Parceiro");
            gridOrders.SetHeader("CardName", "Nome");
            gridOrders.SetHeader("Total", "Total");

            gridOrders.IsReadOnly = true;
        }

        private void FormatGridLines()
        {
            gridLines.SetInvisible("OrderId");

            gridLines.SetHeader("Id", "Id");
            gridLines.SetHeader("ItemCode", "Código");
            gridLines.SetHeader("ItemName", "Descrição");
            gridLines.SetHeader("Price", "Preço");
            gridLines.SetHeader("Quantity", "Quantidade");
            gridLines.SetHeader("Total", "Total");

            gridLines.IsReadOnly = true;
        }

        private void FillGrids()
        {
            gridOrders.ItemsSource = _service.GetAll();
            FillLines();
        }

        private void FillLines()
        {
            var order = gridOrders.SelectedItem as OrderInput;
            if (order is null) return;

            var orderInput = _service.Get(order.Id);

            if (orderInput != null)
                gridLines.ItemsSource = orderInput.Lines;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var form = new OrdersForm(_service);
                form.ShowDialog();
                FillGrids();
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orderTemp = gridOrders.SelectedItem as OrderInput;
                if (orderTemp is null) return;

                var order = _service.Get(orderTemp.Id);
                var form = new OrdersForm(_service, order);
                form.ShowDialog();
                FillGrids();
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!UserConfirm("Deseja remover o pedido selecionado?")) return;

                var orderTemp = gridLines.SelectedItem as OrderInput;
                if (orderTemp is null) return;

                _service.Delete(orderTemp.Id);
                FillGrids();
                FormHelper.MessageSuccess();
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
                if (MessageBoxResult.Yes.Equals(FormHelper.MessageQuestion("Deseja realmente sair da aplicação?")))
                    Close();
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }
    }
}
