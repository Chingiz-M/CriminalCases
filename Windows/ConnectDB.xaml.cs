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

namespace CriminalCases.Windows
{
    /// <summary>
    /// Логика взаимодействия для ConnectDB.xaml
    /// </summary>
    public partial class ConnectDB : Window
    {
        /// <summary>
        /// Имя сервера для подключения
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// Имя Базы данных
        /// </summary>
        public string DataBase { get; set; }
        public ConnectDB()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbDB.Text) && !String.IsNullOrEmpty(tbServer.Text))
            {
                Server = tbServer.Text; // присвоение имя для сервера из TextBox(tbServer)
                DataBase = tbDB.Text;// присвоение базы данных из TextBox(tbDB)
                DialogResult = true;
            }
            else
                MessageBox.Show("Введите данные!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
