using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SalesOrderWpf.UI.Helpers
{
    public static class FormHelper
    {
        public static void MessageError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Mensagem do Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void MessageSuccess(string message = "Operação realizada com sucesso")
        {
            MessageBox.Show(message, "Mensagem do Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void SetHeader(this DataGrid dataGrid, string columnName, string columnDescription)
        {
            var column = dataGrid.Columns.FirstOrDefault(c => columnName.Equals(c.Header.ToString()));

            if (column is null) return;

            column.Header = columnDescription;
        }

        public static void SetInvisible(this DataGrid dataGrid, string columnName)
        {
            var column = dataGrid.Columns.FirstOrDefault(c => columnName.Equals(c.Header.ToString()));

            if (column is null) return;

            column.Visibility = Visibility.Hidden;
        }
    }
}
