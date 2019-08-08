using SalesOrderWpf.Domain.ViewModels;
using SalesOrderWpf.UI.Helpers;
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
using System.Text.RegularExpressions;

namespace SalesOrderWpf.UI
{
    public partial class ItemForm : Window
    {
        public LineInput LineInput;

        public ItemForm(LineInput lineInput)
        {
            InitializeComponent();

            LineInput = lineInput;

            FillControls();
            SignEvents();
        }

        private void SignEvents()
        {
            txtQuantity.PreviewTextInput += TxtQuantity_PreviewTextInput;
            txtQuantity.TextChanged += CalcLineTotalEvent;
            txtPrice.TextChanged += CalcLineTotalEvent;
        }

        private void FillControls()
        {
            txtItemCode.Text = LineInput.ItemCode;
            txtItemName.Text = LineInput.ItemName;
            txtQuantity.Text = decimal.Zero.Equals(LineInput.Quantity) ? string.Empty : LineInput.Quantity.ToString();
            txtPrice.Text = decimal.Zero.Equals(LineInput.Price) ? string.Empty : LineInput.Price.ToString();
            txtLineTotal.Text = LineInput.Total.ToString();
        }

        private void FillClass()
        {
            LineInput.ItemCode = txtItemCode.Text;
            LineInput.ItemName = txtItemName.Text;
            SetQuantityAndPrice();
        }

        private void TxtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                var textBox = sender as TextBox;
                e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void CalcLineTotalEvent(object sender, TextChangedEventArgs e)
        {
            try
            {
                SetQuantityAndPrice();
                txtLineTotal.Text = LineInput.Total.ToString();
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void SetQuantityAndPrice()
        {
            LineInput.Quantity = string.IsNullOrEmpty(txtQuantity.Text) ? 0 : Convert.ToInt32(txtQuantity.Text);
            LineInput.Price = string.IsNullOrEmpty(txtPrice.Text) ? decimal.Zero : Convert.ToDecimal(txtPrice.Text);
        }

        private void CmdOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FillClass();
                FormHelper.MessageSuccess();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                FormHelper.MessageError(ex);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
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
