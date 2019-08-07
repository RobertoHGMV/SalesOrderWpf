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

namespace SalesOrderWpf.UI
{
    public partial class LineForm : Window
    {
        private LineInput _lineInput;

        public LineForm()
        {
            InitializeComponent();
        }

        private void SignEvents()
        {
            txtQuantity.TextChanged += CalcLineTotalEvent;
            txtPrice.TextChanged += CalcLineTotalEvent;
        }

        private void FillControls()
        {
            txtItemCode.Text = _lineInput.ItemCode;
            txtItemName.Text = _lineInput.ItemName;
            txtQuantity.Text = _lineInput.Quantity.ToString();
            txtPrice.Text = _lineInput.Price.ToString();
            txtLineTotal.Text = _lineInput.Total.ToString();
        }

        private void FillClass()
        {
            _lineInput.ItemCode = txtItemCode.Text;
            _lineInput.ItemName = txtItemName.Text;
        }

        private void CalcLineTotalEvent(object sender, TextChangedEventArgs e)
        {
            try
            {
                txtLineTotal.Text = _lineInput.Total.ToString();
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
    }
}
