using SalesOrderWpf.Domain.Services;
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
    public partial class OrdersForm : Window
    {
        IOrderService _service;
        private bool _isClosed = false;
        
        private OrderInput Order;
        public ObservableCollection<LineInput> Lines { get; set; }

        public OrdersForm(IOrderService service)
        {
            InitializeComponent();

            _service = service;
            Order = new OrderInput();
            Lines = new ObservableCollection<LineInput>();

            FillControls();
            SignEvents();
        }

        public OrdersForm(IOrderService service, OrderInput order)
        {
            InitializeComponent();

            _service = service;
            Order = order;
            Lines = new ObservableCollection<LineInput>();
            foreach (var line in order.Lines)
                Lines.Add(line);

            FillControls();
            SignEvents();
        }

        private void SignEvents()
        {
            Loaded += Window_Loaded;
            this.Closed += OrdersForm_Closed;
        }

        private void FillClass()
        {
            Order.CardCode = txtCardCode.Text;
            Order.CardName = txtCardName.Text;
            Order.Lines = Lines;
        }

        private void FillControls()
        {
            txtCardCode.Text = Order.CardCode;
            txtCardName.Text = Order.CardName;
            gridLines.ItemsSource = Lines;
        }

        private void FormatGrid()
        {
            gridLines.SetInvisible("Id");
            gridLines.SetInvisible("OrderId");

            gridLines.SetHeader("ItemCode", "Código");
            gridLines.SetHeader("ItemName", "Descrição");
            gridLines.SetHeader("Price", "Preço");
            gridLines.SetHeader("Quantity", "Quantidade");
            gridLines.SetHeader("Total", "Total");

            gridLines.IsReadOnly = true;
        }

        private void Save()
        {
            FillClass();

            if (string.IsNullOrEmpty(Order.Id))
                _service.Add(Order);
            else
                _service.Update(Order);
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

        private void OrdersForm_Closed(object sender, EventArgs e)
        {
            try
            {
                _isClosed = true;
                //base.OnClosed(e);
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
                var line = new LineInput();
                var form = new ItemForm(line);
                form.Closed += Form_Closed;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void Form_Closed(object sender, EventArgs e)
        {
            try
            {
                var form = sender as ItemForm;
                var dialogResul = form?.DialogResult ?? false;

                if (_isClosed || !dialogResul) return;
                
                Lines.Add(form.LineInput);
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
                var line = gridLines.SelectedItem as LineInput;

                if (line is null)
                    throw new Exception("Nenhuma linha selecionada");

                var form = new ItemForm(line);
                form.ShowDialog();

                var lineToUpdated = Lines.FirstOrDefault(c => line.Id.Equals(c.Id));
                lineToUpdated = line;
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
                var line = gridLines.SelectedItem as LineInput;

                if (line != null)
                    Lines.Remove(line);
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
                if (!UserConfirm("Deseja salvar as alterações?")) return;

                Save();
                FormHelper.MessageSuccess();
                DialogResult = true;
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

        private bool UserConfirm(string message)
        {
            return MessageBoxResult.Yes.Equals(FormHelper.MessageQuestion(message));
        }
    }
}
