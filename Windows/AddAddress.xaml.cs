using CriminalCases.StaticModel;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для AddAddress.xaml
    /// </summary>
    public partial class AddAddress : Window
    {
        public static DbContextOptions Option { get; set; }
        public AddAddress(DbContextOptions option)
        {
            InitializeComponent();
            Option = option;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbAddress.Text = "";
            tbRegion.Text = "";
            tbCountry.Text = "";
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnAdd_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new KPContext(Option))
                {
                    await db.Addresses.AddAsync(new Adress
                    {
                        Address = tbAddress.Text,
                        Region = tbRegion.Text,
                        Country = tbCountry.Text
                    });
                    await db.SaveChangesAsync();
                }
                MessageBox.Show("Адрес добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
