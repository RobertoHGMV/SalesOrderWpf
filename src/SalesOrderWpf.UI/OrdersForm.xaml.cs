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
        private bool _isClosed = false;
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
            this.Closed += OrdersForm_Closed;
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
                form.Closed += Form_Closed;
                form.ShowDialog();
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
    }
}
